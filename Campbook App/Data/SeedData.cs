namespace Campbook_App.Data.SeedData;

public class City
{
    public string? Name { get; set; }
    public string? State { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public static class Cities
{
    public static List<City> All = new()
    {
        new City { Name = "New York", State = "New York", Latitude = 40.7127837, Longitude = -74.0059413 },
        new City { Name = "Los Angeles", State = "California", Latitude = 34.0522342, Longitude = -118.2436849 },
        new City { Name = "Chicago", State = "Illinois", Latitude = 41.8781136, Longitude = -87.6297982 },
        new City { Name = "Houston", State = "Texas", Latitude = 29.7604267, Longitude = -95.3698028 },
        new City { Name = "Phoenix", State = "Arizona", Latitude = 33.4483771, Longitude = -112.0740373 },
    };
}
