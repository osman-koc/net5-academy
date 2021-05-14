using MediatR;
using Microsoft.AspNetCore.Mvc;
using NET5Academy.Services.Order.Application.DDD.Commands;
using NET5Academy.Services.Order.Application.DDD.Queries;
using NET5Academy.Shared.ControllerBases;
using NET5Academy.Shared.Services;
using System.Threading.Tasks;

namespace NET5Academy.Services.Order.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = userId });
            return OkActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var response = await _mediator.Send(command);
            return OkActionResult(response);
        }
    }
}
