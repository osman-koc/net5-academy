using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Discount.Application.Dtos;
using NET5Academy.Services.Discount.Application.Services;
using NET5Academy.Shared.ControllerBases;
using NET5Academy.Shared.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NET5Academy.Services.Discount.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : OkBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _discountService.GetAll();
            return OkActionResult(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([Required] int id)
        {
            var result = await _discountService.GetById(id);
            return OkActionResult(result);
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode([Required] string code)
        {
            var userId = _sharedIdentityService.GetUserId();
            var result = await _discountService.GetByCodeAndUserId(code, userId);
            return OkActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiscountCreateDto dto)
        {
            dto.UserId = _sharedIdentityService.GetUserId();
            var result = await _discountService.Create(dto);
            return OkActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DiscountUpdateDto dto)
        {
            dto.UserId = _sharedIdentityService.GetUserId();
            var result = await _discountService.Update(dto);
            return OkActionResult(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            var result = await _discountService.DeleteById(id);
            return OkActionResult(result);
        }
    }
}
