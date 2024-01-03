using Satellite.Interfaces;
using Satellite.Models;
using System.Text.Json;

public class N2YOSatelliteClient : ISatelliteClient
{
    private const string N2YO_API_BASE_URL = "https://api.n2yo.com/rest/v1/satellite";
    private readonly string N2YO_API_KEY;
    private readonly IHttpClient _httpClient;

    public N2YOSatelliteClient(IHttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        N2YO_API_KEY = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
    }

    public async Task<IList<Above>> GetSatellitesAsync(double latitude, double longitude, int radius)
    {
        try
        {
            string apiUrl = $"{N2YO_API_BASE_URL}/above/{latitude}/{longitude}/0/{radius}/15&apiKey={N2YO_API_KEY}";

            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
                return new List<Above>();

            var data = await response.Content.ReadAsStringAsync();
            var satelliteData = JsonSerializer.Deserialize<SatelliteInfo>(data.Trim());

            if (satelliteData != null && satelliteData.Above != null)
                return satelliteData.Above;

            return new List<Above>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching satellite data: {ex.Message}");
            return new List<Above>();
        }
    }
}

