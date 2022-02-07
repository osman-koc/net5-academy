using NET5Academy.Web.Models.Services;
using System.Threading.Tasks;

namespace NET5Academy.Web.Services
{
    public interface IUserService
    {
        Task<UserModel> GetUser();
    }
}
