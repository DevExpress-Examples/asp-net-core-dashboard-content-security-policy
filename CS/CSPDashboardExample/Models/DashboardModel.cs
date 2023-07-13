using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CSPDashboardExample.Models
{
    public class DashboardModel: PageModel
    {
        public string Nonce { get; set; }

        public DashboardModel() {
            var nonceBytes = new byte[32];
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(nonceBytes);
            Nonce = Convert.ToBase64String(nonceBytes);
        }

        public IActionResult OnGet() {
            HttpContext.Response.Headers.Add("Content-Security-Policy",
                "img-src data: https: http:;" +
                string.Format("script-src 'self' 'nonce-{0}';", Nonce) +
                string.Format("style-src 'self' 'nonce-{0}';", Nonce) 
                    );
            return Page();
        }
    }
}
