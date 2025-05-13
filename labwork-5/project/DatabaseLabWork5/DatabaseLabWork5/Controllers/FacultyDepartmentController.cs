using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class FacultyDepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public FacultyDepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /FacultyDepartment/Index
        public async Task<IActionResult> Index()
        {
            // Get all departments along with the associated faculties
            var departments = await _context.Departments
                .Include(d => d.Faculty)  // Include the faculty for each department
                .ToListAsync();

            return View(departments);
        }

        // GET: /FacultyDepartment/Create
        public IActionResult Create()
        {
            // Get faculties to populate the dropdown
            ViewBag.FacultyList = _context.Faculties.ToList();
            return View();
        }

        // POST: /FacultyDepartment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentName, FacultyID")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(department);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Department added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"An error occurred while saving the department: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            // If validation fails, return to the view with the existing faculty list
            ViewBag.FacultyList = _context.Faculties.ToList();
            return View(department);
        }

        // GET: /FacultyDepartment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }

            // Get faculties to populate the dropdown
            ViewBag.FacultyList = _context.Faculties.ToList();
            return View(department);
        }

        // POST: /FacultyDepartment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID, DepartmentName, FacultyID")] Department department)
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
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Department updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while updating the department: {ex.InnerException?.Message ?? ex.Message}";
                }
            }

            // If validation fails, return to the view with the existing faculty list
            ViewBag.FacultyList = _context.Faculties.ToList();
            return View(department);
        }

        // GET: /FacultyDepartment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Faculty)  // To include the faculty information in case it's needed for confirmation
                .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null)
            {
                TempData["ErrorMessage"] = "Department not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // POST: /FacultyDepartment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Department deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Department not found.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
