using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Services
{
    public class StudentCourseValidationService : IValidationService<StudentCourse>
    {
        private readonly AppDbContext _context;

        public StudentCourseValidationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ValidationResult> ValidateCreateAsync(StudentCourse entity)
        {
            var errors = new List<string>();

            if (entity == null)
                errors.Add("StudentCourse entity cannot be null.");
            else
            {
                // Validate StudentID
                if (!await _context.Students.AnyAsync(s => s.StudentID == entity.StudentID))
                    errors.Add("Invalid Student ID: Student does not exist.");

                // Validate CourseID
                if (!await _context.Courses.AnyAsync(c => c.CourseID == entity.CourseID))
                    errors.Add("Invalid Course ID: Course does not exist.");

                // Check for duplicate enrollment
                if (await _context.StudentCourses.AnyAsync(sc => sc.StudentID == entity.StudentID && sc.CourseID == entity.CourseID))
                    errors.Add("This student is already enrolled in this course.");

                // Validate Year
                if (entity.Year < 2000 || entity.Year > DateTime.Now.Year + 1)
                    errors.Add($"Year must be between 2000 and {DateTime.Now.Year + 1}.");

                // Validate Semester
                var validSemesters = new[] { "Spring", "Summer", "Fall" };
                if (string.IsNullOrWhiteSpace(entity.Semester) || !validSemesters.Contains(entity.Semester))
                    errors.Add("Semester must be one of: Spring, Summer, Fall.");

                // Validate Grades (if provided)
                if (entity.Midterm.HasValue && (entity.Midterm < 0 || entity.Midterm > 100))
                    errors.Add("Midterm grade must be between 0 and 100.");
                if (entity.Final.HasValue && (entity.Final < 0 || entity.Final > 100))
                    errors.Add("Final grade must be between 0 and 100.");
            }

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success;
        }

        public async Task<ValidationResult> ValidateUpdateAsync(StudentCourse entity)
        {
            var errors = new List<string>();

            if (entity == null)
                errors.Add("StudentCourse entity cannot be null.");
            else
            {
                // Validate existence of the record
                var existing = await _context.StudentCourses
                    .FirstOrDefaultAsync(sc => sc.StudentID == entity.StudentID && sc.CourseID == entity.CourseID);
                if (existing == null)
                    errors.Add("Course assignment does not exist.");

                // Validate StudentID
                if (!await _context.Students.AnyAsync(s => s.StudentID == entity.StudentID))
                    errors.Add("Invalid Student ID: Student does not exist.");

                // Validate CourseID
                if (!await _context.Courses.AnyAsync(c => c.CourseID == entity.CourseID))
                    errors.Add("Invalid Course ID: Course does not exist.");

                // Validate Year
                if (entity.Year < 2000 || entity.Year > DateTime.Now.Year + 1)
                    errors.Add($"Year must be between 2000 and {DateTime.Now.Year + 1}.");

                // Validate Semester
                var validSemesters = new[] { "Spring", "Summer", "Fall" };
                if (string.IsNullOrWhiteSpace(entity.Semester) || !validSemesters.Contains(entity.Semester))
                    errors.Add("Semester must be one of: Spring, Summer, Fall.");

                // Validate Grades
                if (entity.Midterm.HasValue && (entity.Midterm < 0 || entity.Midterm > 100))
                    errors.Add("Midterm grade must be between 0 and 100.");
                if (entity.Final.HasValue && (entity.Final < 0 || entity.Final > 100))
                    errors.Add("Final grade must be between 0 and 100.");
            }

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success;
        }

        public async Task<ValidationResult> ValidateDeleteAsync(int id)
        {
            // Note: Since StudentCourse uses composite key (StudentID, CourseID), we'll assume id is not used directly.
            // Instead, we'll validate based on the existence of the record.
            // This method is included for interface compliance but may need adjustment based on usage.
            var errors = new List<string>();
            errors.Add("Delete validation requires StudentID and CourseID. Use specific overload if needed.");
            return ValidationResult.Failure(errors);
        }

        // Overload for composite key validation
        public async Task<ValidationResult> ValidateDeleteAsync(int studentId, int courseId)
        {
            var errors = new List<string>();
            var exists = await _context.StudentCourses.AnyAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
            if (!exists)
                errors.Add("Course assignment does not exist.");

            return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success;
        }
    }
}