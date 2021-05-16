using IdentityModel.Client;
using NET5Academy.Shared.Models;
using NET5Academy.Web.Models.Services;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public interface IIdentityService
    {
        Task<OkResponse<bool>> SignIn(SignInModel model);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
