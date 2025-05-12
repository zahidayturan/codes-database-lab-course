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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ModelState.AddModelError("", "Validation errors: " + string.Join(", ", errors));
            }
            return View(faculty);
        }

        // GET: /Faculty/Index
        public async Task<IActionResult> Index()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return View(faculties);
        }
    }
}