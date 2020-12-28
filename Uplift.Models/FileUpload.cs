using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uplift.Models
{
    [Table("FileUploads")]
    public class FileUpload
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "Upload File(s)")]
        public byte[] FileData { get; set; }
    }
}
