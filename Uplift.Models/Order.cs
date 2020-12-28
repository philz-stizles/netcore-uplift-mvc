using System;
using System.Collections.Generic;

namespace Uplift.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int totalItems { get; set; }
        public double totalPrice { get; set; }
        public DateTime OrderedAt { get; set; }
        public IEnumerable<Service> Items { get; set; }
        public Customer CustomerDetail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}