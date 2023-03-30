using lab1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1.Pages;

public class AntivirusModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public AntivirusService.AntivirusInfo AntivirusInfo { get; set; }

    public AntivirusModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        AntivirusInfo = AntivirusService.GetAntivirusInfo();    
    }
}