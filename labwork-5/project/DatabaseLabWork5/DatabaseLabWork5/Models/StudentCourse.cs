using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLabWork5.Models
{
    // StudentCourse Entity (Junction Table)
    public class StudentCourse
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Semester { get; set; }

        [Range(0, 100)]
        public int? Midterm { get; set; }

        [Range(0, 100)]
        public int? Final { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }


}
