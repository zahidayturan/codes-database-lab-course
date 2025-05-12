using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(Enumerable.Empty<SelectListItem>());
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DepartmentID")] Student student)
        {
            Console.WriteLine($"Received: FirstName={student.FirstName}, LastName={student.LastName}, DepartmentID={student.DepartmentID}");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                    ModelState.AddModelError("", $"An error occurred while saving the student: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
                ModelState.AddModelError("", "Validation errors: " + string.Join(", ", errors));
            }
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(_context.Departments.Where(d => d.FacultyID == _context.Departments.FirstOrDefault(d2 => d2.DepartmentID == student.DepartmentID).FacultyID), "DepartmentID", "DepartmentName", student.DepartmentID);
            return View(student);
        }

        // GET: /Student/Index
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.Department)
                .ThenInclude(d => d.Faculty)
                .ToListAsync();
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            return View(students);
        }

        // POST: /Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,FirstName,LastName,DepartmentID")] Student student)
        {
            if (id != student.StudentID)
            {
                TempData["ErrorMessage"] = "Invalid student ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await _context.Students.FindAsync(id);
                    if (existingStudent == null)
                    {
                        TempData["ErrorMessage"] = "Student not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    existingStudent.FirstName = student.FirstName;
                    existingStudent.LastName = student.LastName;
                    existingStudent.DepartmentID = student.DepartmentID;
                    _context.Update(existingStudent);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = ($"An error occurred while updating the student: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validation errors: " + string.Join(", ", errors);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Student/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while deleting the student: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}