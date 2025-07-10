using Campbook_App.Models;

namespace Campbook_App.Services;

public interface IGeocodingService
{
    Task<Geometry> GeocodeAsync(string location);
}
