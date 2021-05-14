using MediatR;
using NET5Academy.Services.Order.Application.Dtos;
using NET5Academy.Shared.Models;
using System.Collections.Generic;

namespace NET5Academy.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<OkResponse<OrderResponseDto>>
    {
        public string BuyerId { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public CreateOrderCommand()
        {
            OrderItems = new List<OrderItemDto>();
        }
    }
}
