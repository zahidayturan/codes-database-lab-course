using System.ComponentModel.DataAnnotations;

namespace DatabaseLabWork5.Models
{
    public class StudentCourse
    {
        [Key]
        public int StudentID { get; set; }

        [Key]
        public int CourseID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Semester { get; set; }

        public int? Midterm { get; set; }

        public int? Final { get; set; }

        public Student? Student { get; set; }

        public Course? Course { get; set; }
    }
}