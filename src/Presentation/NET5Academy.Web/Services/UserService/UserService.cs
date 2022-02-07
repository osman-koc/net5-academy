using NET5Academy.Web.Models.Config;
using NET5Academy.Web.Models.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly OkServiceSettings _config;
        public UserService(HttpClient httpClient, OkServiceSettings config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<UserModel> GetUser()
        {
            var result = await _httpClient.GetFromJsonAsync<UserModel>(_config.IdentityServer.GetUserPath);
            return result;
        }
    }
}
