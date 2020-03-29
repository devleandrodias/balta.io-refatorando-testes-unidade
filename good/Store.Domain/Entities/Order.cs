using System;
using System.Collections.Generic;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        public Order(Customer customer, decimal deliveryFree, IList<OrderItem> items, Discount discount)
        {
            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.WaitingPayment;
            DeliveryFree = deliveryFree;
            Discount = discount;
            Items = new List<OrderItem>();
        }

        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public IList<OrderItem> Items { get; private set; }
        public decimal DeliveryFree { get; private set; }
        public Discount Discount { get; private set; }
        public EOrderStatus Status { get; set; }

        public void AddItem(Product product, int quantity)
        {
            OrderItem item = new OrderItem(product, quantity);
        }

        public decimal Total()
        {
            decimal total = 0;

            foreach (var item in Items)
                total += item.Total();

            total += DeliveryFree;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                this.Status = EOrderStatus.WaitingDelivery;
        }

        public void Calcel()
        {
            Status = EOrderStatus.Canceled;
        }
    }
}