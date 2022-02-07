using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.Web.Services;
using System.Threading.Tasks;

namespace NET5Academy.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetUser();
            return View(result);
        }
    }
}
