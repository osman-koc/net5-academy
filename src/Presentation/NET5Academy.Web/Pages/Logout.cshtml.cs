using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace NET5Academy.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        public LogoutModel(ILogger<LogoutModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogDebug("Logout - Deleting all cookies..");
            foreach (var item in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(item);
            }
            LocalRedirect("/Login");
        }
    }
}
