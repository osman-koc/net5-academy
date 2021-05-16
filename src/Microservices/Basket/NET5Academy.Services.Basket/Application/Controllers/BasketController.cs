using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Basket.Application.Dtos;
using NET5Academy.Services.Basket.Application.Services;
using NET5Academy.Shared.Controllers;
using NET5Academy.Shared.Services;
using System.Threading.Tasks;

namespace NET5Academy.Services.Basket.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : OkBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        /// <summary>
        /// Get basket by user id from token
        /// </summary>
        /// <returns>BasketDto</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _basketService.GetByUserId(userId);
            return OkActionResult(response);
        }

        /// <summary>
        /// Create or Update basket
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>bool</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromBody] BasketDto dto)
        {
            dto.UserId = _sharedIdentityService.GetUserId();
            var response = await _basketService.CreateOrUpdate(dto);
            return OkActionResult(response);
        }

        /// <summary>
        /// Delete basket by user id from token
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _basketService.DeleteByUserId(userId);
            return OkActionResult(response);
        }
    }
}
