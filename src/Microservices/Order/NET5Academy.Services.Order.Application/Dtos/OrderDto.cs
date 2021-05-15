using System;
using System.Collections.Generic;

namespace NET5Academy.Services.Order.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
