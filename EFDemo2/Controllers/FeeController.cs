
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFDemo2.Models;
using EFDemo2.Service;

public class FeeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICodeGenerator codeGenerator;

    public FeeController(
        ApplicationDbContext context,
        ICodeGenerator codeGenerator)
    {
        _context = context;
        this.codeGenerator = codeGenerator;
    }

    // GET: Fees
    public async Task<IActionResult> Index()
    {
        return View(await _context.Fees.OrderByDescending(x=>x.FeeDate).ToListAsync());
    }

    // GET: Fees/Details/F001
    public async Task<IActionResult> Details(string? feeid)
    {
        if (feeid == null)
        {
            return NotFound();
        }

        var fee = await _context.Fees
            .FirstOrDefaultAsync(m => m.FeeId == feeid);

        if (fee == null)
        {
            return NotFound();
        }

        return View(fee);
    }

    // GET: Fees/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Fees/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("FeeId,Month,Year,FeeDate,Amount,StuId")] Fee fee)
    {
        var TotalFeePaid = _context.Fees.Where(x => x.StuId == fee.StuId).Sum(x => x.Amount);
        var CourseFee = (from s in _context.Students 
                         join c in _context.Courses 
                         on s.CourseId equals c.CourseId 
                         where s.StuId == fee.StuId
                         select new
                         {
                             courseFee=c.Fee
                         }
                         ).FirstOrDefault();
        if(CourseFee != null && TotalFeePaid + fee.Amount > CourseFee.courseFee)
        {
            var dues = CourseFee.courseFee - TotalFeePaid;
            ModelState.AddModelError("Amount" , "Total fee paid exceeds the course fee is Rs "+ dues);
            return View(fee);
        }
        if (ModelState.IsValid)
        {
            _context.Add(fee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(fee);
    }

    // GET: Fees/Edit/F001
    public async Task<IActionResult> Edit(string? feeid)
    {
        if (feeid == null)
        {
            return NotFound();
        }

        var fee = await _context.Fees.FindAsync(feeid);

        if (fee == null)
        {
            return NotFound();
        }

        return View(fee);
    }

    // POST: Fees/Edit/F001
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        string? feeid,
        [Bind("FeeId,Month,Year,FeeDate,Amount,StuId,Student")] Fee fee)
    {
        if (feeid != fee.FeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(fee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(fee.FeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        return View(fee);
    }

    // GET: Fees/Delete/F001
    public async Task<IActionResult> Delete(string? feeid)
    {
        if (feeid == null)
        {
            return NotFound();
        }

        var fee = await _context.Fees
            .FirstOrDefaultAsync(m => m.FeeId == feeid);

        if (fee == null)
        {
            return NotFound();
        }

        return View(fee);
    }

    // POST: Fees/Delete/F001
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? feeid)
    {
        var fee = await _context.Fees.FindAsync(feeid);

        if (fee != null)
        {
            _context.Fees.Remove(fee);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Check whether FeeId exists
    private bool FeeExists(string? feeid)
    {
        return _context.Fees.Any(e => e.FeeId == feeid);
    }
}
