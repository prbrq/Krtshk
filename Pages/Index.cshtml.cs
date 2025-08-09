using Krtshk.Models;
using Krtshk.Repositories;
using Krtshk.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Krtshk.Pages;

public class IndexModel(ILinkRepository linkRepository, IKeyService keyService, IConfiguration configuration) : PageModel
{
    public string ShortUrl { get; private set; } = "";

    public async Task OnPostAsync(string url)
    {
        var link = new Link
        {
            Key = keyService.Key(12),
            Url = url
        };

        await linkRepository.AddLinkAsync(link);

        var baseUrl = configuration["BaseUrl"] ?? ""; // TODO: Обработать null

        ShortUrl = string.Concat(baseUrl, "/", link.Key);
    }

    public async Task<IActionResult> OnGetAsync(string? key)
    {
        if (key is null)
        {
            return Page();
        }

        var link = await linkRepository.GetLinkAsync(key); // TODO: Обработать null

        return Redirect(link.Url);
    }
}