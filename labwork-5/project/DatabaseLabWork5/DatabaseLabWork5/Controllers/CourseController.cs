using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Course/Create
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }

        // POST: /Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseName,DepartmentID")] Course course)
        {
            Console.WriteLine($"Received: CourseName={course.CourseName}, DepartmentID={course.DepartmentID}");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Course added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                    ModelState.AddModelError("", $"An error occurred while saving the course: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
                ModelState.AddModelError("", "Validation errors: " + string.Join(", ", errors));
            }
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(_context.Departments.Where(d => d.FacultyID == _context.Departments.FirstOrDefault(d2 => d2.DepartmentID == course.DepartmentID).FacultyID), "DepartmentID", "DepartmentName", course.DepartmentID);
            return View(course);
        }

        // GET: /Course/Index
        public async Task<IActionResult> Index(int? year, string semester)
        {
            var viewModel = new CourseIndexViewModel
            {
                Courses = new List<Course>(),
                EnrollmentInfos = new List<CourseEnrollmentInfo>(),
                YearOptions = new List<SelectListItem>(),
                SemesterOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "All Semesters" },
                    new SelectListItem { Value = "Spring", Text = "Spring" },
                    new SelectListItem { Value = "Summer", Text = "Summer" },
                    new SelectListItem { Value = "Fall", Text = "Fall" }
                },
                SelectedYear = year,
                SelectedSemester = semester
            };

            // Yıl seçeneklerini çek
            var years = await _context.StudentCourses
                .Select(sc => sc.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();
            viewModel.YearOptions.Add(new SelectListItem { Value = "", Text = "All Years" });
            foreach (var y in years)
            {
                viewModel.YearOptions.Add(new SelectListItem { Value = y.ToString(), Text = y.ToString() });
            }

            // Kursları ve filtreleri uygula
            var coursesQuery = _context.Courses
                .Include(c => c.Department)
                .ThenInclude(d => d.Faculty)
                .AsQueryable();

            if (year.HasValue || !string.IsNullOrEmpty(semester))
            {
                var filteredCourseIds = await _context.StudentCourses
                    .Where(sc => (!year.HasValue || sc.Year == year.Value) &&
                                 (string.IsNullOrEmpty(semester) || sc.Semester == semester))
                    .Select(sc => sc.CourseID)
                    .Distinct()
                    .ToListAsync();
                coursesQuery = coursesQuery.Where(c => filteredCourseIds.Contains(c.CourseID));
            }

            viewModel.Courses = await coursesQuery.OrderBy(c => c.CourseName).ToListAsync();

            // Her kurs için yıl/dönem bilgisi (istemci tarafında)
            foreach (var course in viewModel.Courses)
            {
                var enrollments = await _context.StudentCourses
                    .Where(sc => sc.CourseID == course.CourseID)
                    .Select(sc => new { sc.Year, sc.Semester })
                    .Distinct()
                    .ToListAsync();

                var enrollmentStrings = enrollments
                    .Select(e => $"{e.Year} {e.Semester}")
                    .OrderBy(e => e)
                    .ToList();

                viewModel.EnrollmentInfos.Add(new CourseEnrollmentInfo
                {
                    CourseID = course.CourseID,
                    Enrollments = enrollmentStrings.Any() ? enrollmentStrings : new List<string> { "No enrollments" }
                });
            }

            return View(viewModel);
        }


        // POST: /Course/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id)
        {
            return await GetCourseDetails(id);
        }

        // Ortak detay alma metodu
        private async Task<IActionResult> GetCourseDetails(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .ThenInclude(d => d.Faculty)
                .FirstOrDefaultAsync(c => c.CourseID == id);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction(nameof(Index));
            }

            var studentCourses = await _context.StudentCourses
                .Where(sc => sc.CourseID == id)
                .Include(sc => sc.Student)
                .ToListAsync();

            var totalStudentCount = studentCourses.Count;

            var studentGroups = studentCourses
                .GroupBy(sc => new { sc.Year, sc.Semester })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Semester)
                .Select(g => new CourseDetailsViewModel.StudentGroup
                {
                    Year = g.Key.Year,
                    Semester = g.Key.Semester,
                    Students = g.Select(sc => new CourseDetailsViewModel.StudentCourseInfo
                    {
                        StudentID = sc.StudentID,
                        FirstName = sc.Student.FirstName,
                        LastName = sc.Student.LastName,
                        Midterm = sc.Midterm,
                        Final = sc.Final,
                        Result = sc.Midterm.HasValue && sc.Final.HasValue
                            ? sc.Midterm.Value * 0.4 + sc.Final.Value * 0.6
                            : null
                    }).ToList(),
                    StudentCount = g.Count(),
                    AverageMidterm = g.Where(sc => sc.Midterm.HasValue).Any()
                        ? g.Where(sc => sc.Midterm.HasValue).Average(sc => sc.Midterm.Value)
                        : null,
                    AverageFinal = g.Where(sc => sc.Final.HasValue).Any()
                        ? g.Where(sc => sc.Final.HasValue).Average(sc => sc.Final.Value)
                        : null,
                    AverageResult = g.Where(sc => sc.Midterm.HasValue && sc.Final.HasValue).Any()
                        ? g.Where(sc => sc.Midterm.HasValue && sc.Final.HasValue)
                            .Average(sc => sc.Midterm.Value * 0.4 + sc.Final.Value * 0.6)
                        : null
                })
                .ToList();

            var model = new CourseDetailsViewModel
            {
                Course = course,
                TotalStudentCount = totalStudentCount,
                StudentGroups = studentGroups
            };

            return View(model);
        }

        // POST: /Course/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseID,CourseName,DepartmentID")] Course course)
        {
            if (id != course.CourseID)
            {
                TempData["ErrorMessage"] = "Invalid course ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCourse = await _context.Courses.FindAsync(id);
                    if (existingCourse == null)
                    {
                        TempData["ErrorMessage"] = "Course not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    existingCourse.CourseName = course.CourseName;
                    existingCourse.DepartmentID = course.DepartmentID;
                    _context.Update(existingCourse);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Course updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = ($"An error occurred while updating the course: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validation errors: " + string.Join(", ", errors);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Course/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course deleted successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while deleting the course: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Course/GetByDepartment
        [HttpGet]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var courses = await _context.Courses
                .Where(c => c.DepartmentID == departmentId)
                .Select(c => new { c.CourseID, c.CourseName })
                .ToListAsync();
            return Json(courses);
        }
    }
}