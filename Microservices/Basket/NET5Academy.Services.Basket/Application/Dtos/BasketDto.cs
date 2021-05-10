using System.Collections.Generic;
using System.Linq;

namespace NET5Academy.Services.Basket.Application.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public decimal TotalPrice { get => Items.Sum(x => x.Price * x.Quantity); }
        public List<BasketItemDto> Items { get; set; }

        public BasketDto()
        {
            Items = new List<BasketItemDto>();
        }
    }
}
