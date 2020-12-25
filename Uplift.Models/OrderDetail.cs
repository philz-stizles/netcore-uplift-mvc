using System;
using System.Collections.Generic;

namespace Uplift.Models
{
    public class OrderDetail
    {
        public string Status { get; set; }
        public string Comment { get; set; }
        public int totalItems { get; set; }
        public DateTime OrderedAt { get; set; }
        public IEnumerable<Service> Items { get; set; }
    }
}