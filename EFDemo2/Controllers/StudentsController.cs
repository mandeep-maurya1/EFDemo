using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFDemo2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: STUDENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Students.ToListAsync());
    }

    // GET: STUDENTS/Details/5
    public async Task<IActionResult> Details(string? Id)
    {
        if (string.IsNullOrEmpty(Id))
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Course)
            .Include(s => s.Fees)
            .Include(s => s.Photo)
            .FirstOrDefaultAsync(s => s.StuId == Id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: STUDENTS/Create
    public async Task<IActionResult> Create()
    {
        var Courses = await _context.Courses.ToListAsync();
        ViewBag.Courses = new SelectList(Courses, "CourseId", "CourseName");
        return View();
    }

    // POST: STUDENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("StuId,Name,Gender,DOB,Email,Phone,CourseId")] Student student)
    {
        // Check whether Student ID already exists
        bool studentExists = await _context.Students
            .AnyAsync(s => s.StuId == student.StuId);

        if (studentExists)
        {
            ModelState.AddModelError(
                "StuId",
                "This Student ID already exists. Please enter a different Student ID."
            );
        }

        if (ModelState.IsValid)
        {
            student.Name = student.Name?.ToUpper();
            student.Email = student.Email?.ToLower();
            _context.Add(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student registered successfully!";
            return RedirectToAction(nameof(Index));
        }

        var courses = await _context.Courses.ToListAsync();
        ViewBag.Courses = new SelectList(
            courses, "CourseId", "CourseName", student.CourseId);

        return View(student);
    }

    // GET: STUDENTS/Edit/5
    public async Task<IActionResult> Edit(string? Id)
    {
        if (Id == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(Id);

        var Courses = await _context.Courses.ToListAsync();
        ViewBag.Courses = new SelectList(Courses, "CourseId", "CourseName");
        if (student == null)
        {
            return NotFound();
        }


        return View(student);
    }

    // POST: STUDENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? Id, [Bind("StuId,Name,Gender,DOB,Email,Phone,CourseId")] Student student)
    {
        if (Id != student.StuId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
                    && sqlEx.Number == 547)
                {
                    ModelState.AddModelError(
                        "CourseId",
                        "This Course ID does not exist. Please select a valid Course."
                    );
                }
                else
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Unable to update student. Please try again."
                    );
                }

                return View(student);
            }
            var courses = await _context.Courses.ToListAsync();
            ViewBag.Courses = new SelectList(
                courses, "CourseId", "CourseName", student.CourseId);

            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: STUDENTS/Delete/5
    public async Task<IActionResult> Delete(string? Id)
    {
        if (Id == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StuId == Id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: STUDENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? Id)
    {
        var student = await _context.Students.FindAsync(Id);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(string? Id)
    {
        return _context.Students.Any(e => e.StuId == Id);
    }
}
