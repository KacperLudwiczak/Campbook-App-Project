using Microsoft.AspNetCore.Identity;

namespace Campbook_App.Models;

public class ApplicationUser : IdentityUser
{
    // Add custom properties (like email is already built-in)
    [PersonalData]
    public string? DisplayName { get; set; } // Optional

    // Optional: Navigation to reviews & campgrounds
    public List<Review> Reviews { get; set; } = new();
    public List<Campground> Campgrounds { get; set; } = new();
}
