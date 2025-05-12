using DatabaseLabWork5.Data;
using DatabaseLabWork5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Controllers
{
    public class FacultyController : Controller
    {
        private readonly AppDbContext _context;

        public FacultyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Faculty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Faculty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyName")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(faculty);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Faculty added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"An error occurred while saving the faculty: {ex.InnerException?.Message ?? ex.Message}");
                }
            }
            return View(faculty);
        }

        // GET: /Faculty/Index
        public async Task<IActionResult> Index()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return View(faculties);
        }

        // POST: /Faculty/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyID,FacultyName")] Faculty faculty)
        {
            if (id != faculty.FacultyID)
            {
                TempData["ErrorMessage"] = "Invalid faculty ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFaculty = await _context.Faculties.FindAsync(id);
                    if (existingFaculty == null)
                    {
                        TempData["ErrorMessage"] = "Faculty not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    existingFaculty.FacultyName = faculty.FacultyName;
                    _context.Update(existingFaculty);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Faculty updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while updating the faculty: {ex.InnerException?.Message ?? ex.Message}";
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Validation errors: " + string.Join(", ", errors);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Faculty/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null)
            {
                TempData["ErrorMessage"] = "Faculty not found.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Faculty deleted successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the faculty: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}