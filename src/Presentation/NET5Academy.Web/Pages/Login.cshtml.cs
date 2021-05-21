using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NET5Academy.Web.Services;

namespace NET5Academy.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IIdentityService _identityService;
        public LoginModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [BindProperty]
        public string Username { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect("/admin");

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Message = "Kullanýcý adý veya parola alaný geçersiz.";
                return Page();
            }

            var signinModel = new Models.Services.SignInModel
            {
                Email = Username,
                Password = Password,
                RememberMe = false
            };

            var response = await _identityService.SignInAsync(signinModel);
            if (!response.IsSuccess)
            {
                Message = response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.NotFound
                    ? "Kullanýcý adý veya parola yanlýþ."
                    : "Sisteme giriþ yapýlamadý";
                return Page();
            }

            return Redirect("/admin");
        }
    }
}
