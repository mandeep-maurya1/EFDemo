using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDemo2.Models
{
    public class Fee
    {
        [Key]
        [DisplayName("Fee Id")]
        public string FeeId { get; set; }

        [Required(ErrorMessage = "Enter Months")]
        public string? Month { get; set; }

        [Required(ErrorMessage = "Enter Year")]
        public string? Year { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [DisplayName("Fee Date")]
        public DateOnly FeeDate { get; set; }

        [Required(ErrorMessage = "Enter Amount")]
        public int Amount { get; set; }

        public string? StuId { get; set; }

        [ForeignKey(nameof(StuId))]
        public Student? Student { get; set; }
    }
}