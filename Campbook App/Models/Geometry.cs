// Models/Geometry.cs
public class Geometry
{
    public string Type { get; set; } = "Point"; // Always "Point"
    public List<double> Coordinates { get; set; } = new(); // [lng, lat]
}
