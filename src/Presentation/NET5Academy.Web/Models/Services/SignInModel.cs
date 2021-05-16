using System.ComponentModel.DataAnnotations;

namespace NET5Academy.Web.Models.Services
{
    public class SignInModel
    {
        [Display(Name = "E-posta adresi")]
        public string Email { get; set; }

        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
