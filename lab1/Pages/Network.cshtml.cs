using lab1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab1.Pages;

public class NetworkModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public NetworkService.NetworkAdaptersInfo NetworkAdaptersInfo { get; set; }

    public NetworkModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        NetworkAdaptersInfo = NetworkService.GetNetworkAdaptersInfo();
    }
}