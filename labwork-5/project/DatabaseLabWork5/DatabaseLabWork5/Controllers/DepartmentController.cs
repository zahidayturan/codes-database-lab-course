using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Department/Create
        public IActionResult Create()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        // POST: /Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentName,FacultyID")] Department department)
        {
            Console.WriteLine($"Received: DepartmentName={department.DepartmentName}, FacultyID={department.FacultyID}");
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("ModelState is valid, saving to database...");
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Department added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                    ModelState.AddModelError("", $"An error occurred while saving the department: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("ModelState errors: " + string.Join(", ", errors));
                ModelState.AddModelError("", "Validation errors: " + string.Join(", ", errors));
            }
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName", department.FacultyID);
            return View(department);
        }

        // GET: /Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Faculty)
                .ToListAsync();
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View(departments);
        }

        // POST: /Department/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,DepartmentName,FacultyID")] Department department)
        {
            if (id != department.DepartmentID)
            {
                TempData["ErrorMessage"] = "Invalid department ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDepartment = await _context.Departments.FindAsync(id);
                    if (existingDepartment == null)
                    {
                        TempData["ErrorMessage"] = "Department not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    existingDepartment.DepartmentName = department.DepartmentName;
                    existingDepartment.FacultyID = department.FacultyID;
                    _context.Update(existingDepartment);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Department updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = ($"An error occurred while updating the department: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validation errors: " + string.Join(", ", errors);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Department/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Department deleted successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while deleting the department: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Department/GetByFaculty
        [HttpGet]
        public async Task<IActionResult> GetByFaculty(int facultyId)
        {
            var departments = await _context.Departments
                .Where(d => d.FacultyID == facultyId)
                .Select(d => new { d.DepartmentID, d.DepartmentName })
                .ToListAsync();
            return Json(departments);
        }
    }
}