using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(100)]
        public string FacultyName { get; set; }

        public List<Department>? Departments { get; set; } // Nullable to avoid required validation
    }
}