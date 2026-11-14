using EFDemo2.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo2.ViewModel
{
    public class PhotoViewModel
    {
        [Key]
        [DisplayName("Photo Id")]
        public int PhotoId { get; set; }
        [Required(ErrorMessage = "Please select a student.")]
        public IFormFile PhotoPath { get; set; }
        [Required(ErrorMessage = "Please select a photo.")]
        public string? StuId { get; set; }
        [ForeignKey("StuId")]
        public Student? Student { get; set; }
    }
}
