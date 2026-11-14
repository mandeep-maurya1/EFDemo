
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFDemo2.Models;
using EFDemo2.ViewModel;
using Microsoft.AspNetCore.Identity;

public class PhotosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment env;
    public PhotosController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        this.env = env;
    }

    // GET: PHOTOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Photos.ToListAsync());
    }

    // GET: PHOTOS/Details/5
    public async Task<IActionResult> Details(int? photoid)
    {
        if (photoid == null)
        {
            return NotFound();
        }

        var photo = await _context.Photos
            .FirstOrDefaultAsync(m => m.PhotoId == photoid);
        if (photo == null)
        {
            return NotFound();
        }

        return View(photo);
    }

    // GET: PHOTOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PHOTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PhotoViewModel photo)
    {
        string filePath = null;


        if (!ModelState.IsValid)
            return View(photo);
        if (photo.PhotoPath.Length > (1024 * 1024 * 2))
        {
            ModelState.AddModelError("", "Image Size Under 2MB");
            return View(photo);
        }
        var allowExtention = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".webp" };
        var imageExtention = Path.GetExtension(photo.PhotoPath.FileName);
        if (allowExtention.Contains(imageExtention) != true)
        {
            ModelState.AddModelError("", "Only jpg, jpeg, png, bmp and webp image are allowed");
            return View(photo);
        }

        try
        {
            string fn = Guid.NewGuid().ToString()
                        + Path.GetExtension(photo.PhotoPath.FileName);

            string folderPath = Path.Combine(env.WebRootPath, "StudentPhoto");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            filePath = Path.Combine(folderPath, fn);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.PhotoPath.CopyToAsync(stream);
            }

            Photo image = new Photo
            {
                PhotoPath = fn,
                StuId = photo.StuId
            };

            _context.Add(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {
            // Remove uploaded file if database insertion fails
            if (filePath != null && System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx
                && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                ModelState.AddModelError(
                    "StuId",
                    "This student already has a photo. Please select another student."
                );
            }
            else
            {
                ModelState.AddModelError(
                    "",
                    "Unable to save photo. Please try again."
                );
            }

            return View(photo);
        }
        catch (Exception)
        {
            if (filePath != null && System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            ModelState.AddModelError("", "Something went wrong. Please try again.");

            return View(photo);
        }
    }


    // GET: PHOTOS/Edit/5
    public async Task<IActionResult> Edit(int? photoid)
    {
        if (photoid == null)
        {
            return NotFound();
        }

        var photo = await _context.Photos.FindAsync(photoid);
        if (photo == null)
        {
            return NotFound();
        }
        return View(photo);
    }

    // POST: PHOTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? photoid, [Bind("PhotoId,PhotoPath,StuId,Student")] Photo photo)
    {
        if (photoid != photo.PhotoId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(photo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(photo.PhotoId))
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
        return View(photo);
    }

    // GET: PHOTOS/Delete/5
    public async Task<IActionResult> Delete(int? photoid)
    {
        if (photoid == null)
        {
            return NotFound();
        }

        var photo = await _context.Photos
            .FirstOrDefaultAsync(m => m.PhotoId == photoid);
        if (photo == null)
        {
            return NotFound();
        }

        return View(photo);
    }

    // POST: PHOTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? photoid)
    {
        var photo = await _context.Photos.FindAsync(photoid);
        if (photo != null)
        {
            _context.Photos.Remove(photo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PhotoExists(int? photoid)
    {
        return _context.Photos.Any(e => e.PhotoId == photoid);
    }
}
