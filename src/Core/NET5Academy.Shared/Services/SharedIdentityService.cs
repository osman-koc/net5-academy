using Microsoft.AspNetCore.Http;
using NET5Academy.Shared.Constants;

namespace NET5Academy.Shared.Services
{
    /// <summary>
    /// Add services.AddHttpContextAccessor() in Startup to use SharedIdentityService
    /// </summary>
    public class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SharedIdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId() => _contextAccessor?.HttpContext?.User?.FindFirst(OkIdentityConstants.UserIdKey)?.Value;
    }
}
