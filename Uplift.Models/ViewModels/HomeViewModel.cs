using System.Collections.Generic;

namespace Uplift.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}