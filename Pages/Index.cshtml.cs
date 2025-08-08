using Krtshk.Models;
using Krtshk.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Krtshk.Pages;

public class IndexModel(ILinkRepository linkRepository) : PageModel
{
    public string Uuid { get; set; } = string.Empty;

    public async Task OnPostAsync(string url)
    {
        var link = new Link
        {
            Key = Guid.CreateVersion7().ToString(),
            Url = url
        };

        await linkRepository.AddLinkAsync(link);

        Uuid = link.Key;
    }

    public async Task<IActionResult> OnGetUrlAsync(string key)
    {
        var link = await linkRepository.GetLinkAsync(key);

        return Redirect(link.Url);
    }
}