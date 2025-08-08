using Krtshk.Models;
using Krtshk.Repositories;
using Krtshk.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Krtshk.Pages;

public class IndexModel(ILinkRepository linkRepository, IKeyService keyService) : PageModel
{
    public string Uuid { get; set; } = string.Empty;

    public async Task OnPostAsync(string url)
    {
        var link = new Link
        {
            Key = keyService.Key(12),
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