namespace Campbook_App.Models;

public class Geometry
{
    public string Type { get; set; } = "Point";
    public List<double> Coordinates { get; set; } = new();
}
