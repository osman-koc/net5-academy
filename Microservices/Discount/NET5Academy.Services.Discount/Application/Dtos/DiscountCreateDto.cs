using System;

namespace NET5Academy.Services.Discount.Application.Dtos
{
    public class DiscountCreateDto
    {
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
