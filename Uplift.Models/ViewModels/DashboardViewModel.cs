using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uplift.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Service> Services { get; set; }
    }
}