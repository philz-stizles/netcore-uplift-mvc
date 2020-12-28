using System.Collections.Generic;

namespace Uplift.Models.ViewModels
{
    public class CartSummaryVM
    {
        public Customer CustomerDetails { get; set; }
        public List<Service> Services { get; set; }
    }
}