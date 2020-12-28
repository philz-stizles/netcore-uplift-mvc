using System.ComponentModel.DataAnnotations;

namespace Uplift.Models
{
    public class Address: Model
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
        
        public string PostalCode { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}