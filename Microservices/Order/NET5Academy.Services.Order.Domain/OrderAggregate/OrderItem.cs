using NET5Academy.Shared.Domain;

namespace NET5Academy.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : OkEntity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ImageUrl { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem() { }
        public OrderItem(string productId, string productName, string imageUrl, decimal price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ImageUrl = imageUrl;
            Price = price;
            Quantity = quantity;
        }

        public void Update(string productName, string imageUrl, decimal price, int quantity)
        {
            ProductName = productName;
            ImageUrl = imageUrl;
            Price = price;
            Quantity = quantity;
        }
    }
}
