using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NET5Academy.Web.Services;
using System.Threading.Tasks;

namespace NET5Academy.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly IIdentityService _identityService;
        public LogoutModel(ILogger<LogoutModel> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        public async Task OnGet()
        {
            _logger.LogDebug("Logout - Deleting all cookies..");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _identityService.RevokeRefreshToken();

            //foreach (var item in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(item);
            //}
        }
    }
}
