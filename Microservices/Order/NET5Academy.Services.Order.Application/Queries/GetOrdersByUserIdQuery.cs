using MediatR;
using NET5Academy.Services.Order.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;

namespace NET5Academy.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<OkResponse<IEnumerable<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
