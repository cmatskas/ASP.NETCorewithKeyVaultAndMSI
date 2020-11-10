using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASPCoreWithKV.Pages
{
    public class IndexModel : PageModel
    {

        public string ConfigSecret { get; set; }
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
