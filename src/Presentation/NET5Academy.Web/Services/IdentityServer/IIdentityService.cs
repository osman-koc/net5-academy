using IdentityModel.Client;
using NET5Academy.Shared.Models;
using NET5Academy.Web.Models.Services;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public interface IIdentityService
    {
        Task<OkResponse<bool>> SignInAsync(SignInModel model);
        Task<TokenResponse> GetAccessTokenByRefreshTokenAsync();
        Task<OkResponse<bool>> RevokeRefreshTokenAsync();
    }
}
