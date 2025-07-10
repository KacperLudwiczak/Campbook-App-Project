using System.Text.Json;
using Campbook_App.Models;

namespace Campbook_App.Services;

public class MapboxGeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;

    public MapboxGeocodingService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _accessToken = configuration["Mapbox:AccessToken"]
            ?? throw new ArgumentNullException("Mapbox access token not configured.");
    }

    public async Task<Geometry> GeocodeAsync(string location)
    {
        var url = $"https://api.mapbox.com/geocoding/v5/mapbox.places/{Uri.EscapeDataString(location)}.json?access_token={_accessToken}&limit=1";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var coords = json
            .GetProperty("features")[0]
            .GetProperty("geometry")
            .GetProperty("coordinates");

        return new Geometry
        {
            Coordinates = new[]
            {
                coords[0].GetDouble(), // longitude
                coords[1].GetDouble()  // latitude
            }
        };
    }
}

