using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Campbook_App.Data;

[Authorize]
public class ReviewsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // POST: /Campgrounds/{id}/Reviews
    [HttpPost]
    public async Task<IActionResult> Create(int id, [Bind("Body,Rating")] Review review)
    {
        var campground = await _context.Campgrounds
            .Include(c => c.Reviews)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campground == null) return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }
        review.AuthorId = user.Id;
        review.CampgroundId = id;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        TempData["success"] = "Created new review!";
        return RedirectToAction("Details", "Campgrounds", new { id });
    }

    // POST: /Campgrounds/{id}/Reviews/{reviewId}/Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id, int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review == null || review.CampgroundId != id)
            return NotFound();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        TempData["success"] = "Successfully deleted review!";
        return RedirectToAction("Details", "Campgrounds", new { id });
    }
}
