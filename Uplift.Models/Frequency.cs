using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplift.Models
{
    [Table("Frequencies")]
    public class Frequency: Model
    {
        [Required]
        [Display(Name="Frequency Name")]
        public string Name { get; set; }
        [Required]
        public int FrequencyCount { get; set; }
    }
}
