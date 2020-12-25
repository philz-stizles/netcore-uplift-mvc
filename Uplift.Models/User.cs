using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Uplift.Models
{
    [Table("ApplicationUsers")]
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Address> Address { get; set; }
    }
}
