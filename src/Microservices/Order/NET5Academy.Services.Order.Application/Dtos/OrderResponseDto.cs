namespace NET5Academy.Services.Order.Application.Dtos
{
    public class OrderResponseDto
    {
        public int OrderId { get; private set; }
        public OrderResponseDto(int orderId)
        {
            OrderId = orderId;
        }
    }
}
