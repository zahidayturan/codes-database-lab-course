using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [Required]
        public int FacultyID { get; set; }

        public Faculty? Faculty { get; set; }

        public List<Student>? Students { get; set; }
    }
}