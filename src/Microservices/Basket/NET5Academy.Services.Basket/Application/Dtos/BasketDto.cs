using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace NET5Academy.Services.Basket.Application.Dtos
{
    public class BasketDto
    {
        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public decimal TotalPrice { get => Items.Sum(x => x.Price * x.Quantity); }

        public string DiscountCode { get; set; }

        public List<BasketItemDto> Items { get; set; }

        public BasketDto()
        {
            Items = new List<BasketItemDto>();
        }
    }
}
