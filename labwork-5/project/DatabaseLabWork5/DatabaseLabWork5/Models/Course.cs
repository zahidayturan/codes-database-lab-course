using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public Department? Department { get; set; }

        public List<StudentCourse>? StudentCourses { get; set; }
    }
}