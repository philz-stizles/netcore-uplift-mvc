using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Uplift.Models.ViewModels
{
    public class ServiceViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Frequencies { get; set; }
        public Service Service { get; set; }
    }
}