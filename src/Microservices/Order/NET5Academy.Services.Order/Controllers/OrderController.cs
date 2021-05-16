using MediatR;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Order.Application.DDD.Commands;
using NET5Academy.Services.Order.Application.DDD.Queries;
using NET5Academy.Shared.Controllers;
using NET5Academy.Shared.Services;
using System.Threading.Tasks;

namespace NET5Academy.Services.Order.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : OkBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;
        public OrderController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List OrderDto</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = userId });
            return OkActionResult(response);
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="command">CreateOrderCommand</param>
        /// <returns>OrderResponseDto/returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return OkActionResult(response);
        }
    }
}
