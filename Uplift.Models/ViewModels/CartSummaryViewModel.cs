using System.Collections.Generic;

namespace Uplift.Models.ViewModels
{
    public class CartSummaryViewModel
    {
        public CustomerModel CustomerDetails { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}