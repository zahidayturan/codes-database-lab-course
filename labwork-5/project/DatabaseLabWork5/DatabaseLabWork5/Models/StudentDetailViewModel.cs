using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
        public SelectList AvailableCourses { get; set; }
    }
}