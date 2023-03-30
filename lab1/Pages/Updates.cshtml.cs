using System.Text.Json;
using lab1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1.Pages;

public class UpdatesModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public UpdatesService.UpdatesInfo UpdatesInfo { get; set; }

    public UpdatesModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {   
        Console.WriteLine("string" + JsonSerializer.Serialize(UpdatesService.GetUpdatesInfo().Updates));
        UpdatesInfo = UpdatesService.GetUpdatesInfo();
    }
}