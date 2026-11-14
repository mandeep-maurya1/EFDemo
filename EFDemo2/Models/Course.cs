using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFDemo2.Models
{
    public class Course
    {
        [Key]
        [RegularExpression(@"^[C\d+]{4}", ErrorMessage = "Course Id must be in format C123.")]
        [Required(ErrorMessage ="Course Id can't be blank...")]
        [DisplayName("Course Id")]
        public string? CourseId { get; set; }
        [Required (ErrorMessage="Please Enter Course Name")]
        [DisplayName("Course Name")]
        public string? CourseName { get; set; }
        [DisplayName("Course Duration")]
        [Required(ErrorMessage ="Enter Duration Details...")]
        public string? CourseDuration { get; set; }
        [DisplayName("Course Fee")]
        [Required(ErrorMessage = "Enter Fee...")]
        public int Fee { get; set; }
        public ICollection<Student>? Students { get; set; }=new List <Student>();
    }
}
