using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Krtshk.Pages;

public class IndexModel : PageModel
{
    public Guid Uuid { get; set; }

    private static readonly Dictionary<Guid, string> Urls = [];

    public void OnPost(string url)
    {
        var uuid = Guid.CreateVersion7();

        Urls[uuid] = url;

        Uuid = uuid;
    }

    public IActionResult OnGetUrl(Guid uuid)
    {
        if (Urls.TryGetValue(uuid, out var url) == false)
        {
            return NotFound();
        }

        return Redirect(url);
    }
}