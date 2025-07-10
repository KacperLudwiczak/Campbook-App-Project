using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campbook_App.Models;

public class Campground
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    public List<Image> Images { get; set; } = new();

    public Geometry? Geometry { get; set; }

    [Range(0, 10000)]
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    // Navigation Properties
    public string? AuthorId { get; set; } // FK to Identity User
    public ApplicationUser? Author { get; set; }

    public List<Review> Reviews { get; set; } = new();

    // Virtual: PopUpMarkup (Razor helper can use this instead)
    [NotMapped]
    public string PopUpMarkup => $@"
        <strong><a href=""/campgrounds/{Id}"">{Title}</a></strong>
        <p>{Description?.Substring(0, Math.Min(20, Description.Length))}...</p>";
}
