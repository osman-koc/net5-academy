using MediatR;
using Microsoft.EntityFrameworkCore;
using NET5Academy.Services.Order.Application.Dtos;
using NET5Academy.Services.Order.Application.Mapping;
using NET5Academy.Services.Order.Application.Queries;
using NET5Academy.Services.Order.Infrastructure;
using NET5Academy.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, OkResponse<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;
        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<OkResponse<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync(cancellationToken);
            if (!orders.Any())
                orders = new List<Domain.OrderAggregate.Order>();

            var orderDtos = OkObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return OkResponse<List<OrderDto>>.Success(HttpStatusCode.OK, orderDtos);
        }
    }
}
