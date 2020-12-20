using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplift.Models
{
    [Table("Categories")]
    public class Category: Model
    {
        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string DisplayOrder { get; set; }
    }
}
