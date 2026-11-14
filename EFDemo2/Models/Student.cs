using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo2.Models
{
    public class Student
    {
        [Key]
        [DisplayName("Student Id")]
        public string? StuId { get; set; }
        [DisplayName("Student Name")]
        [Required(ErrorMessage ="Enter Student Name")]
        public string? Name { get; set; }
        [DisplayName ("Student Gender")]
        [Required(ErrorMessage ="Please Select Gender")]
        public string? Gender { get; set; }
        [DisplayName ("Date Of Birth")]
        [Required(ErrorMessage ="Please Enter DOB")]
        public DateOnly DOB {  get; set; }
        [EmailAddress(ErrorMessage ="Please Enter Valid Email Id")]
        public string? Email { get; set; }
        [Phone(ErrorMessage="Please Enter Valid Phone Number")]
        [DisplayName("Mobile Name")]
        public string? Phone { get; set; }
        [ForeignKey("CourseId")]
        public string? CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Fee> Fees { get; set; } = new List<Fee>();
        public Photo? Photo { get; set; }
    }
}
