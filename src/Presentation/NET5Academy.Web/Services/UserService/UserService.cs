using NET5Academy.Web.Models.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public class UserService : IUserService
    {
        private const string GET_USER_URI = "/api/user/getuser";
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> GetUser()
        {
            var result = await _httpClient.GetFromJsonAsync<UserModel>(GET_USER_URI);
            return result;
        }
    }
}
