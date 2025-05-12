using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLabWork5.Models
{
    // Department Entity
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }

        public List<Student> Students { get; set; }
    }
}
