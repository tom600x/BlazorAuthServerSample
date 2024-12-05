using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BlazorAuthServerSample.Pages
{

    public class MyBaseClass : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public MyBaseClass(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

 
    }
}
