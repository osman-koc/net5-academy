using MediatR;
using NET5Academy.Services.Order.Application.DDD.Commands;
using NET5Academy.Services.Order.Application.Dtos;
using NET5Academy.Services.Order.Application.Mapping;
using NET5Academy.Services.Order.Domain.OrderAggregate;
using NET5Academy.Services.Order.Infrastructure;
using NET5Academy.Shared.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NET5Academy.Services.Order.Application.DDD.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OkResponse<OrderResponseDto>>
    {
        private readonly OrderDbContext _context;
        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<OkResponse<OrderResponseDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = OkObjectMapper.Mapper.Map<Address>(request.Address);
            var newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.ImageUrl, x.Quantity);
            });

            await _context.Orders.AddAsync(newOrder, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new OrderResponseDto(newOrder.Id);
            return OkResponse<OrderResponseDto>.Success(HttpStatusCode.OK, response);
        }
    }
}
