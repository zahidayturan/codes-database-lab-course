using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using DatabaseLabWork5.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IValidationService<StudentCourse> _validationService;

        public StudentCourseController(AppDbContext context, IValidationService<StudentCourse> validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        // GET: /StudentCourse/Index
        public async Task<IActionResult> Index(int? studentId, int? courseId, int? year, string semester)
        {
            var query = _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.Faculty)
                .AsQueryable();

            if (studentId.HasValue)
                query = query.Where(sc => sc.StudentID == studentId.Value);
            if (courseId.HasValue)
                query = query.Where(sc => sc.CourseID == courseId.Value);
            if (year.HasValue)
                query = query.Where(sc => sc.Year == year.Value);
            if (!string.IsNullOrEmpty(semester))
                query = query.Where(sc => sc.Semester == semester);

            var studentCourses = await query.OrderBy(sc => sc.Student.LastName).ThenBy(sc => sc.Course.CourseName).ToListAsync();

            ViewBag.StudentList = new SelectList(
                await _context.Students.OrderBy(s => s.LastName).Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }).ToListAsync(),
                "StudentID", "FullName", studentId);
            ViewBag.CourseList = new SelectList(
                await _context.Courses.OrderBy(c => c.CourseName).Select(c => new { c.CourseID, c.CourseName }).ToListAsync(),
                "CourseID", "CourseName", courseId);
            ViewBag.YearOptions = new SelectList(
                await _context.StudentCourses.Select(sc => sc.Year).Distinct().OrderBy(y => y).ToListAsync(),
                year);
            ViewBag.SemesterOptions = new SelectList(
                new[] { "Spring", "Summer", "Fall" }.Select(s => new { Value = s, Text = s }),
                "Value", "Text", semester);

            ViewBag.SelectedStudentId = studentId;
            ViewBag.SelectedCourseId = courseId;
            ViewBag.SelectedYear = year;
            ViewBag.SelectedSemester = semester;

            return View(studentCourses);
        }

        // GET: /StudentCourse/Create
        public IActionResult Create()
        {
            ViewBag.StudentList = new SelectList(
                _context.Students.OrderBy(s => s.LastName).Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }),
                "StudentID", "FullName");
            ViewBag.CourseList = new SelectList(
                _context.Courses.OrderBy(c => c.CourseName).Select(c => new { c.CourseID, c.CourseName }),
                "CourseID", "CourseName");
            ViewBag.SemesterList = new SelectList(
                new[] { "Spring", "Summer", "Fall" },
                "Spring");
            return View(new StudentCourse { Year = DateTime.Now.Year });
        }

        // POST: /StudentCourse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,CourseID,Year,Semester")] StudentCourse studentCourse)
        {
            var validationResult = await _validationService.ValidateCreateAsync(studentCourse);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(studentCourse);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Course assigned successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"An error occurred while assigning the course: {ex.InnerException?.Message ?? ex.Message}");

                }
            }

            ViewBag.StudentList = new SelectList(
                _context.Students.OrderBy(s => s.LastName).Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }),
                "StudentID", "FullName", studentCourse.StudentID);
            ViewBag.CourseList = new SelectList(
                _context.Courses.OrderBy(c => c.CourseName).Select(c => new { c.CourseID, c.CourseName }),
                "CourseID", "CourseName", studentCourse.CourseID);
            ViewBag.SemesterList = new SelectList(
                new[] { "Spring", "Summer", "Fall" },
                studentCourse.Semester);
            return View(studentCourse);
        }

        // GET: /StudentCourse/Details
        public async Task<IActionResult> Details(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .ThenInclude(c => c.Department)
                .ThenInclude(d => d.Faculty)
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);

            if (studentCourse == null)
            {
                TempData["ErrorMessage"] = "Course assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(studentCourse);
        }

        // GET: /StudentCourse/Edit
        public async Task<IActionResult> Edit(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);

            if (studentCourse == null)
            {
                TempData["ErrorMessage"] = "Course assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.StudentList = new SelectList(
                _context.Students.OrderBy(s => s.LastName).Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }),
                "StudentID", "FullName", studentCourse.StudentID);
            ViewBag.CourseList = new SelectList(
                _context.Courses.OrderBy(c => c.CourseName).Select(c => new { c.CourseID, c.CourseName }),
                "CourseID", "CourseName", studentCourse.CourseID);
            ViewBag.SemesterList = new SelectList(
                new[] { "Spring", "Summer", "Fall" },
                studentCourse.Semester);
            return View(studentCourse);
        }

        // POST: /StudentCourse/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int studentId, int courseId, [Bind("StudentID,CourseID,Year,Semester,Midterm,Final")] StudentCourse studentCourse)
        {
            if (studentId != studentCourse.StudentID || courseId != studentCourse.CourseID)
            {
                TempData["ErrorMessage"] = "Invalid student or course ID.";
                return RedirectToAction(nameof(Index));
            }

            var validationResult = await _validationService.ValidateUpdateAsync(studentCourse);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudentCourse = await _context.StudentCourses
                        .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
                    if (existingStudentCourse == null)
                    {
                        TempData["ErrorMessage"] = "Course assignment not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    existingStudentCourse.Year = studentCourse.Year;
                    existingStudentCourse.Semester = studentCourse.Semester;
                    existingStudentCourse.Midterm = studentCourse.Midterm;
                    existingStudentCourse.Final = studentCourse.Final;

                    _context.Update(existingStudentCourse);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Course assignment updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ($"An error occurred while updating the course assignment: {ex.InnerException?.Message ?? ex.Message}"));
                }
            }

            ViewBag.StudentList = new SelectList(
                _context.Students.OrderBy(s => s.LastName).Select(s => new { s.StudentID, FullName = s.LastName + ", " + s.FirstName }),
                "StudentID", "FullName", studentCourse.StudentID);
            ViewBag.CourseList = new SelectList(
                _context.Courses.OrderBy(c => c.CourseName).Select(c => new { c.CourseID, c.CourseName }),
                "CourseID", "CourseName", studentCourse.CourseID);
            ViewBag.SemesterList = new SelectList(
                new[] { "Spring", "Summer", "Fall" },
                studentCourse.Semester);
            return View(studentCourse);
        }

        // POST: /StudentCourse/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int studentId, int courseId)
        {
            var validationResult = await ((StudentCourseValidationService)_validationService).ValidateDeleteAsync(studentId, courseId);
            if (!validationResult.IsValid)
            {
                TempData["ErrorMessage"] = string.Join("; ", validationResult.Errors);
                return RedirectToAction(nameof(Index));
            }

            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
            if (studentCourse == null)
            {
                TempData["ErrorMessage"] = "Course assignment not found.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.StudentCourses.Remove(studentCourse);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course assignment removed successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while removing the course assignment: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}