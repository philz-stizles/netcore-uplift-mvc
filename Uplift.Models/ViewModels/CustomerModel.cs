using System.ComponentModel.DataAnnotations;

namespace Uplift.Models.ViewModels
{
    public class CustomerModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
    }
}