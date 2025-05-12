using System.Collections.Generic;

namespace DatabaseLabWork5.Models
{
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; }
        public int TotalStudentCount { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }

        public class StudentGroup
        {
            public int Year { get; set; }
            public string Semester { get; set; }
            public List<StudentCourseInfo> Students { get; set; }
            public int StudentCount { get; set; }
            public double? AverageMidterm { get; set; }
            public double? AverageFinal { get; set; }
            public double? AverageResult { get; set; }
        }

        public class StudentCourseInfo
        {
            public int StudentID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Midterm { get; set; }
            public int? Final { get; set; }
            public double? Result { get; set; }
        }
    }
}