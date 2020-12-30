using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplift.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderReference { get; set; }
        public OrderStatus Status { get; set; }
        public string Comment { get; set; }
        public int totalItems { get; set; }
        public double totalPrice { get; set; }
        public DateTime OrderedAt { get; set; } = DateTime.Now;
        public ICollection<OrderService> OrderServices { get; set; }
        public Customer CustomerDetail { get; set; }
        [NotMapped]
        public string OrderStatus {
            get
            {
                return Status.ToString();
            }
        }
    }
}