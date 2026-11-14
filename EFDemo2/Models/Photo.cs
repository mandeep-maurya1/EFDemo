using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo2.Models
{
    public class Photo
    {
        [Key]
        [DisplayName("Photo Id")]
        public int PhotoId { get; set; }
        [Required(ErrorMessage = "Please select a student.")]
        public string? PhotoPath { get; set; }
        [Required(ErrorMessage = "Please select a photo.")]
        public string? StuId { get; set; }
        [ForeignKey("StuId")]
        public Student? Student { get; set; }
    }
}
