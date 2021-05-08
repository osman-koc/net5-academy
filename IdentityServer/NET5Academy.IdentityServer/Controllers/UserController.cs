using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.IdentityServer.Data.Entities;
using NET5Academy.IdentityServer.Dtos;
using NET5Academy.Shared.ControllerBases;
using NET5Academy.Shared.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NET5Academy.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : OkBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            var newUser = new ApplicationUser(dto.Email, dto.UserName);
            var result = await _userManager.CreateAsync(newUser, dto.Password);

            var response = result != null && result.Succeeded
                ? OkResponse<object>.Success(HttpStatusCode.Created)
                : OkResponse<object>.Error(HttpStatusCode.BadRequest, result?.Errors?.Select(x => x.Description).ToList());

            return OkActionResult(response);
        }
    }
}
