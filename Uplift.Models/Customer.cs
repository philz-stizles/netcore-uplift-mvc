using System.ComponentModel.DataAnnotations;

namespace Uplift.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public Address Address { get; set; }
    }
}