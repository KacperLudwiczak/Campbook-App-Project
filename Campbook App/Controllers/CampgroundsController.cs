using Microsoft.AspNetCore.Mvc;
using Campbook_App.Data;
using Campbook_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Campbook_App.Services;

public class CampgroundsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IGeocodingService _geocodingService;
    private readonly ICloudinaryService _cloudinaryService;

    public CampgroundsController(ApplicationDbContext context, IGeocodingService geocodingService, ICloudinaryService cloudinaryService)
    {
        _context = context;
        _geocodingService = geocodingService;
        _cloudinaryService = cloudinaryService;
    }

    // GET: /Campgrounds
    public async Task<IActionResult> Index()
    {
        var campgrounds = await _context.Campgrounds
            .Include(c => c.Author)
            .ToListAsync();
        return View(campgrounds);
    }

    // GET: /Campgrounds/New
    [Authorize]
    public IActionResult New()
    {
        return View();
    }

    // POST: /Campgrounds/Create
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Campground campground, List<IFormFile> images)
    {
        if (string.IsNullOrWhiteSpace(campground.Location))
        {
            ModelState.AddModelError("Location", "Location is required.");
            return View("New", campground); // Or whatever view you return
        }
        var geoData = await _geocodingService.GeocodeAsync(campground.Location);
        campground.Geometry = geoData;

        foreach (var file in images)
        {
            var image = await _cloudinaryService.UploadAsync(file);
            campground.Images.Add(image);
        }

        campground.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        _context.Campgrounds.Add(campground);
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully created a new campground!";
        return RedirectToAction("Show", new { id = campground.Id });
    }

    // GET: /Campgrounds/{id}
    public async Task<IActionResult> Show(int id)
    {
        var campground = await _context.Campgrounds
            .Include(c => c.Author)
            .Include(c => c.Reviews)
                .ThenInclude(r => r.Author)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campground == null)
        {
            TempData["error"] = "Cannot find that campground!";
            return RedirectToAction("Index");
        }

        return View(campground);
    }

    // GET: /Campgrounds/Edit/{id}
    [Authorize]
    public async Task<IActionResult> Edit(int id)
    {
        var campground = await _context.Campgrounds.FindAsync(id);
        if (campground == null)
        {
            TempData["error"] = "Cannot find that campground!";
            return RedirectToAction("Index");
        }

        return View(campground);
    }

    // POST: /Campgrounds/Update/{id}
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(int id, Campground updatedCampground, List<IFormFile> images, List<string> deleteImages)
    {
        var campground = await _context.Campgrounds
            .Include(c => c.Images)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campground == null) return NotFound();

        campground.Title = updatedCampground.Title;
        campground.Location = updatedCampground.Location;
        campground.Description = updatedCampground.Description;

        foreach (var file in images)
        {
            var img = await _cloudinaryService.UploadAsync(file);
            campground.Images.Add(img);
        }

        if (deleteImages != null && deleteImages.Any())
        {
            foreach (var filename in deleteImages)
            {
                await _cloudinaryService.DeleteAsync(filename);
                campground.Images.RemoveAll(i => i.Filename == filename);
            }
        }

        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully updated campground!";
        return RedirectToAction("Show", new { id = campground.Id });
    }

    // POST: /Campgrounds/Delete/{id}
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var campground = await _context.Campgrounds.FindAsync(id);
        if (campground == null) return NotFound();

        _context.Campgrounds.Remove(campground);
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully deleted campground!";
        return RedirectToAction("Index");
    }
}
