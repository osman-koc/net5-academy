using Microsoft.AspNetCore.Identity;

namespace NET5Academy.IdentityServer.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }
        public ApplicationUser(string email, string userName) 
        {
            Email = email;
            UserName = userName;
        }
        public ApplicationUser(string userName)
        {
            UserName = userName;
        }
    }
}
