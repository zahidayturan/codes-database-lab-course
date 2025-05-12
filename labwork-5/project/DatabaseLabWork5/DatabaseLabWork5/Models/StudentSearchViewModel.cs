using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLabWork5.Models
{
    public class StudentSearchViewModel
    {
        public int? StudentID { get; set; }

        public int? FacultyID { get; set; }

        public int? DepartmentID { get; set; }

        public string? Name { get; set; }

        public List<Student>? SearchResults { get; set; }
    }
}