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
            Uuid = Guid.CreateVersion7().ToString(),
            Url = url
        };

        await linkRepository.AddLinkAsync(link);

        Uuid = link.Uuid;
    }

    public async Task<IActionResult> OnGetUrlAsync(Guid uuid)
    {
        var link = await linkRepository.GetLinkAsync(uuid);

        return Redirect(link.Url);
    }
}