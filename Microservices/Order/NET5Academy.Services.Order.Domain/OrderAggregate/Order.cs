using NET5Academy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NET5Academy.Services.Order.Domain.OrderAggregate
{
    public class Order : OkEntity, IAggregateRoot
    {
        public string BuyerId { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(string buyerId, Address address)
        {
            BuyerId = buyerId;
            CreatedDate = DateTime.UtcNow;
            Address = address;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(string productId, string productName, decimal price, string imageUrl, int quantity)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, imageUrl, price, quantity);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
