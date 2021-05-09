using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using NET5Academy.IdentityServer.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.IdentityServer.Application.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var errors = new Dictionary<string, object>();

            var existUser = await _userManager.FindByEmailAsync(context.UserName);
            if(existUser == null)
            {
                errors.Add("Errors", new List<string> { "User not found!" });
                context.Result.CustomResponse = errors;
                return;
            }

            var userCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (!userCheck)
            {
                errors.Add("Errors", new List<string> { "Incorrect email or password!" });
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(existUser.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
