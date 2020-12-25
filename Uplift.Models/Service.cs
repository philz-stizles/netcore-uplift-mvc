using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplift.Models
{
    [Table("Services")]
    public class Service: Model
    {
        [Required]
        [Display(Name="Service Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name="Image")]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        // [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        public int FrequencyId { get; set; }

        // [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }
    }
}
