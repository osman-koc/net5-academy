using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.IdentityServer.Data.Entities;
using NET5Academy.IdentityServer.Application.Dtos;
using NET5Academy.Shared.Controllers;
using NET5Academy.Shared.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4;
using System.IdentityModel.Tokens.Jwt;

namespace NET5Academy.IdentityServer.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
                return NotFound();
            
            var userDto = new UserDto(existUser.Id, existUser.UserName, existUser.Email, existUser.EmailConfirmed);
            return Ok(userDto);
        }
    }
}
