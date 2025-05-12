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
                if (ModelState.ContainsKey("DepartmentID") && ModelState["DepartmentID"].Errors.Any())
                {
                    ModelState.AddModelError("DepartmentID", "Please select a department.");
                }
                if (ModelState.ContainsKey("FirstName") && ModelState["FirstName"].Errors.Any())
                {
                    ModelState.AddModelError("FirstName", "Please enter a first name.");
                }
                if (ModelState.ContainsKey("LastName") && ModelState["LastName"].Errors.Any())
                {
                    ModelState.AddModelError("LastName", "Please enter a last name.");
                }
            }
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            // İlk olarak, DepartmentID'nin null olup olmadığını kontrol edelim
            var facultyId = _context.Departments
                                    .FirstOrDefault(d => d.DepartmentID == student.DepartmentID)?
                                    .FacultyID;

            if (facultyId != null)
            {
                ViewBag.DepartmentList = new SelectList(
                    _context.Departments.Where(d => d.FacultyID == facultyId),
                    "DepartmentID",
                    "DepartmentName",
                    student.DepartmentID
                );
            }
            else
            {
                // Eğer facultyId null ise, boş bir liste ile ViewBag.DepartmentList'i ayarlayalım.
                ViewBag.DepartmentList = new SelectList(Enumerable.Empty<SelectListItem>());
            }
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

        // GET: /Student/Search
        public IActionResult Search()
        {
            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            ViewBag.DepartmentList = new SelectList(Enumerable.Empty<SelectListItem>());
            return View(new StudentSearchViewModel());
        }

        // POST: /Student/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(StudentSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["ErrorMessage"] = "Please provide at least one search criterion.";
                ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
                ViewBag.DepartmentList = new SelectList(_context.Departments.Where(d => d.FacultyID == model.FacultyID), "DepartmentID", "DepartmentName", model.DepartmentID);
                return View(model);
            }

            var query = _context.Students
                .Include(s => s.Department)
                .ThenInclude(d => d.Faculty)
                .AsQueryable();

            if (model.StudentID.HasValue)
            {
                query = query.Where(s => s.StudentID == model.StudentID.Value);
            }
            if (model.FacultyID.HasValue)
            {
                query = query.Where(s => s.Department.FacultyID == model.FacultyID.Value);
            }
            if (model.DepartmentID.HasValue)
            {
                query = query.Where(s => s.DepartmentID == model.DepartmentID.Value);
            }
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                query = query.Where(s => s.FirstName.Contains(model.Name) || s.LastName.Contains(model.Name));
            }

            var students = await query.ToListAsync();
            model.SearchResults = students;

            if (!students.Any())
            {
                TempData["InfoMessage"] = "No students found matching the criteria.";
            }

            ViewBag.FacultyList = new SelectList(_context.Faculties, "FacultyID", "FacultyName", model.FacultyID);
            ViewBag.DepartmentList = new SelectList(_context.Departments.Where(d => d.FacultyID == model.FacultyID), "DepartmentID", "DepartmentName", model.DepartmentID);
            return View(model);
        }

        // GET: /Student/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .Include(s => s.Department)
                .ThenInclude(d => d.Faculty)
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefaultAsync(s => s.StudentID == id);

            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction(nameof(Search));
            }

            var model = new StudentDetailsViewModel
            {
                Student = student,
                StudentCourses = student.StudentCourses ?? new List<StudentCourse>(),
                AvailableCourses = new SelectList(_context.Courses.Where(c => c.DepartmentID == student.DepartmentID), "CourseID", "CourseName")
            };

            return View(model);
        }

        // POST: /Student/AssignCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCourse(int studentId, int courseId, int year, string semester)
        {
            if (year < 2000 || year > DateTime.Now.Year + 1)
            {
                TempData["ErrorMessage"] = "Invalid year.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }
            if (string.IsNullOrWhiteSpace(semester))
            {
                TempData["ErrorMessage"] = "Semester is required.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }

            var existingAssignment = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
            if (existingAssignment != null)
            {
                TempData["ErrorMessage"] = "This course is already assigned to the student.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }

            var studentCourse = new StudentCourse
            {
                StudentID = studentId,
                CourseID = courseId,
                Year = year,
                Semester = semester
            };

            try
            {
                _context.StudentCourses.Add(studentCourse);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course assigned successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while assigning the course: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Details), new { id = studentId });
        }

        // POST: /Student/UpdateGrades
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGrades(int studentId, int courseId, int? midterm, int? final)
        {
            if (midterm.HasValue && (midterm < 0 || midterm > 100))
            {
                TempData["ErrorMessage"] = "Midterm grade must be between 0 and 100.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }
            if (final.HasValue && (final < 0 || final > 100))
            {
                TempData["ErrorMessage"] = "Final grade must be between 0 and 100.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }

            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);

            if (studentCourse == null)
            {
                TempData["ErrorMessage"] = "Course assignment not found.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }

            try
            {
                bool isUpdated = false;

                // Midterm notu değişti mi?
                if (midterm.HasValue)
                {
                    if (!studentCourse.Midterm.HasValue || studentCourse.Midterm != midterm)
                    {
                        studentCourse.Midterm = midterm;
                        isUpdated = true;
                    }
                }

                // Final notu değişti mi?
                if (final.HasValue)
                {
                    if (!studentCourse.Final.HasValue || studentCourse.Final != final)
                    {
                        studentCourse.Final = final;
                        isUpdated = true;
                    }
                }

                // Eğer bir değişiklik varsa, veritabanını güncelle
                if (isUpdated)
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Grades updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "No changes detected to update.";
                }
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while updating grades: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction(nameof(Details), new { id = studentId });
        }



        // POST: /Student/RemoveCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCourse(int studentId, int courseId)
        {
            var studentCourse = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CourseID == courseId);
            if (studentCourse == null)
            {
                TempData["ErrorMessage"] = "Course assignment not found.";
                return RedirectToAction(nameof(Details), new { id = studentId });
            }

            try
            {
                _context.StudentCourses.Remove(studentCourse);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course removed successfully!";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = ($"An error occurred while removing the course: {ex.InnerException?.Message ?? ex.Message}");
            }

            return RedirectToAction(nameof(Details), new { id = studentId });
        }
    }
}