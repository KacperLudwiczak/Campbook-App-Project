using Campbook_App.Models;

namespace Campbook_App.Data.SeedData;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Campgrounds.Any()) return;

        var random = new Random();

        foreach (var city in Cities.All)
        {
            var campground = new Campground
            {
                Title = $"Campground in {city.Name}",
                Location = $"{city.Name}, {city.State}",
                Description = "Beautiful campground with nature trails, a river, and lots of peace and quiet.",
                Price = random.Next(10, 50),
                Geometry = new Geometry
                {
                    Type = "Point",
                    Coordinates = new List<double> { city.Longitude, city.Latitude }
                },
                Images = new List<Image>
                {
                    new Image
                    {
                        Url = "https://res.cloudinary.com/demo/image/upload/sample.jpg",
                        Filename = "sample.jpg"
                    }
                }
            };

            context.Campgrounds.Add(campground);
        }

        await context.SaveChangesAsync();
    }
}
