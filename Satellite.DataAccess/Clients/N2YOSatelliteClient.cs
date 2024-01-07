using Satellite.Interfaces;
using Satellite.Models;
using System.Text.Json;

public enum SatelliteType
{
    AmateurRadio = 18,
    BeidouNavigationSystem = 35,
    Brightest = 1,
    Celestis = 45,
    ChineseSpaceStation = 54,
    CubeSats = 32,
    DisasterMonitoring = 8,
    EarthResources = 6,
    Education = 29,
    Engineering = 28,
    Experimental = 19,
    Flock = 48,
    Galileo = 22,
    Geodetic = 27,
    Geostationary = 10,
    GlobalPositioningSystemConstellation = 50,
    GlobalPositioningSystemOperational = 20,
    Globalstar = 17,
    GlonassConstellation = 51,
    GlonassOperational = 21,
    GOES = 5,
    Gonets = 40,
    Gorizont = 12,
    Intelsat = 11,
    Iridium = 15,
    IRNSS = 46,
    ISS = 2,
    Lemur = 49,
    Military = 30,
    Molniya = 14,
    NavyNavigationSatelliteSystem = 24,
    NOAA = 4,
    O3BNetworks = 43,
    OneWeb = 53,
    Orbcomm = 16,
    Parus = 38,
    QZSS = 47,
    RadarCalibration = 31,
    Raduga = 13,
    RussianLEONavigation = 25,
    SatelliteBasedAugmentationSystem = 23,
    SearchAndRescue = 7,
    SpaceAndEarthScience = 26,
    Starlink = 52,
    Strela = 39,
    TrackingAndDataRelaySatelliteSystem = 9,
    Tselina = 44,
    Tsikada = 42,
    Tsiklon = 41,
    TV = 34,
    Weather = 3,
    WestfordNeedles = 37,
    XMAndSirius = 33,
    Yaogan = 36
}


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

    public async Task<IList<Above>> GetSatellitesAsync(double latitude, double longitude, int radius, int type)
    {
        try
        {
            string apiUrl = $"{N2YO_API_BASE_URL}/above/{latitude}/{longitude}/0/{radius}/{type}&apiKey={N2YO_API_KEY}";

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

