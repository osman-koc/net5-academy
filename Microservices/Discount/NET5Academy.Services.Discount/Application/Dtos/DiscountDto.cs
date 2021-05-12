using System;
using System.Text.Json.Serialization;

namespace NET5Academy.Services.Discount.Application.Dtos
{
    public class DiscountDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
