using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class CourseIndexViewModel
    {
        public List<Course> Courses { get; set; }
        public List<CourseEnrollmentInfo> EnrollmentInfos { get; set; }
        public int? SelectedYear { get; set; }
        public string SelectedSemester { get; set; }
        public List<SelectListItem> YearOptions { get; set; }
        public List<SelectListItem> SemesterOptions { get; set; }
    }

    public class CourseEnrollmentInfo
    {
        public int CourseID { get; set; }
        public List<string> Enrollments { get; set; } // Ör: ["2025 Summer", "2024 Fall"]
    }
}