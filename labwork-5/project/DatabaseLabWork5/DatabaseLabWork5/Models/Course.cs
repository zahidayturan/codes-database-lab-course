using System.ComponentModel.DataAnnotations;

namespace DatabaseLabWork5.Models
{
    // Course Entity
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }

}
