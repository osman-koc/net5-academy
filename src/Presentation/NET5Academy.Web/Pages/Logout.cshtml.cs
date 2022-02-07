using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NET5Academy.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        public LogoutModel(ILogger<LogoutModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            _logger.LogDebug("Logout - Deleting all cookies..");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //foreach (var item in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(item);
            //}
        }
    }
}
