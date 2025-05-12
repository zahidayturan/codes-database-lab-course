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
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .ThenInclude(d => d.Faculty)
                .ToListAsync();
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            return View(courses);
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
    }
}