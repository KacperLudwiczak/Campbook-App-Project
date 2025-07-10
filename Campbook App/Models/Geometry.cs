namespace Campbook_App.Models;

public class Geometry
{
    public string Type { get; set; } = "Point";
    public double[] Coordinates { get; set; } = Array.Empty<double>();
}
