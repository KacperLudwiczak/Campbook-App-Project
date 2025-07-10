using System.ComponentModel.DataAnnotations;

namespace Campbook_App.Models;

public class Review
{
    public int Id { get; set; }

    [Required]
    public string Body { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; }

    // Foreign Key to User
    public string AuthorId { get; set; } = null!;
    public ApplicationUser Author { get; set; } = null!;

    // Optional: FK to Campground (if you want reverse navigation)
    public int CampgroundId { get; set; }
    public Campground Campground { get; set; } = null!;
}