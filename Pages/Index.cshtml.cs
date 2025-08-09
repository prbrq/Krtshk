using Krtshk.Models;
using Krtshk.Repositories;
using Krtshk.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Krtshk.Pages;

public class IndexModel(
    ILinkRepository linkRepository,
    IKeyService keyService,
    IConfiguration configuration
) : PageModel
{
    public string ShortUrl { get; private set; } = "";

    public async Task<IActionResult> OnPostAsync(string url)
    {
        if (!IsUrlValid(url))
        {
            return RedirectToPage("Ooops");
        }

        var link = new Link
        {
            Key = keyService.Key(10),
            Url = url
        };

        await linkRepository.AddLinkAsync(link);

        var baseUrl = configuration["BaseUrl"] ?? "";

        ShortUrl = string.Concat(baseUrl, "/?key=", link.Key);

        return Page();
    }

    private static bool IsUrlValid(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var _);
    }

    public async Task<IActionResult> OnGetAsync(string? key)
    {
        if (key is null)
        {
            return Page();
        }

        var link = await linkRepository.GetLinkAsync(key);

        if (link is null)
        {
            return RedirectToPage("Ooops");
        }

        return Redirect(link.Url);
    }
}