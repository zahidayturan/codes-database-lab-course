using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        public Department? Department { get; set; }

        public List<StudentCourse>? StudentCourses { get; set; }
    }
}