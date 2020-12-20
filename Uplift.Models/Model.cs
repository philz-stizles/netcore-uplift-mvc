using System;
using System.ComponentModel.DataAnnotations;

namespace Uplift.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt{ get; set; }
        public string ModifiedBy { get; set; }
        public string IsDeleted { get; set; }
    }
}