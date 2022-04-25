using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPCoreWithKV.Pages
{
    public class IndexModel : PageModel
    {

        public string? ConfigSecret { get; init; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            ConfigSecret = configuration["SomeConfigValueFromKV"]; 
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
