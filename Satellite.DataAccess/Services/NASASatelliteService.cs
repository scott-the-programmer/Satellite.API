using Microsoft.Extensions.Caching.Memory;
using Satellite.Models;
namespace Satellite.DataAccess.Services
{
    public class SatelliteService : ISatelliteService
    {
        private readonly ISatelliteClient _nasaSatelliteClient;
        private readonly IMemoryCache _cache;
        private readonly CurrentCoords _coords;

        public SatelliteService(ISatelliteClient nasaSatelliteClient, IMemoryCache cache, CurrentCoords coords)
        {
            _nasaSatelliteClient = nasaSatelliteClient;
            _cache = cache;
            _coords = coords;
        }

        public async Task<IEnumerable<Models.Satellite>> GetWeatherStationsAsync() =>
            await GetSatellitesAsync(SatelliteType.Weather, "weather");

        public async Task<IEnumerable<Models.Satellite>> GetISSAsync() =>
            await GetSatellitesAsync(SatelliteType.ISS, "iss");

        public async Task<IEnumerable<Models.Satellite>> GetStarlinksAsync() =>
            await GetSatellitesAsync(SatelliteType.Starlink, "starlink");

        public async Task<IEnumerable<Models.Satellite>> GetIridiumsAsync() =>
            await GetSatellitesAsync(SatelliteType.Iridium, "iridium");

        public async Task<IEnumerable<Models.Satellite>> GetAmateurRadioAsync() =>
            await GetSatellitesAsync(SatelliteType.AmateurRadio, "amateur_radio");

        public async Task<IEnumerable<Models.Satellite>> GetBeidouNavigationSystemAsync() =>
            await GetSatellitesAsync(SatelliteType.BeidouNavigationSystem, "beidou_navigation_system");

        public async Task<IEnumerable<Models.Satellite>> GetBrightestAsync() =>
            await GetSatellitesAsync(SatelliteType.Brightest, "brightest");

        public async Task<IEnumerable<Models.Satellite>> GetCelestisAsync() =>
            await GetSatellitesAsync(SatelliteType.Celestis, "celestis");

        public async Task<IEnumerable<Models.Satellite>> GetChineseSpaceStationAsync() =>
            await GetSatellitesAsync(SatelliteType.ChineseSpaceStation, "chinese_space_station");

        public async Task<IEnumerable<Models.Satellite>> GetCubeSatsAsync() =>
            await GetSatellitesAsync(SatelliteType.CubeSats, "cubesats");

        public async Task<IEnumerable<Models.Satellite>> GetDisasterMonitoringAsync() =>
            await GetSatellitesAsync(SatelliteType.DisasterMonitoring, "disaster_monitoring");

        public async Task<IEnumerable<Models.Satellite>> GetEarthResourcesAsync() =>
            await GetSatellitesAsync(SatelliteType.EarthResources, "earth_resources");

        public async Task<IEnumerable<Models.Satellite>> GetEducationAsync() =>
            await GetSatellitesAsync(SatelliteType.Education, "education");

        public async Task<IEnumerable<Models.Satellite>> GetEngineeringAsync() =>
            await GetSatellitesAsync(SatelliteType.Engineering, "engineering");

        public async Task<IEnumerable<Models.Satellite>> GetExperimentalAsync() =>
            await GetSatellitesAsync(SatelliteType.Experimental, "experimental");

        public async Task<IEnumerable<Models.Satellite>> GetFlockAsync() =>
            await GetSatellitesAsync(SatelliteType.Flock, "flock");

        public async Task<IEnumerable<Models.Satellite>> GetGalileoAsync() =>
            await GetSatellitesAsync(SatelliteType.Galileo, "galileo");

        public async Task<IEnumerable<Models.Satellite>> GetGeodeticAsync() =>
            await GetSatellitesAsync(SatelliteType.Geodetic, "geodetic");

        public async Task<IEnumerable<Models.Satellite>> GetGeostationaryAsync() =>
            await GetSatellitesAsync(SatelliteType.Geostationary, "geostationary");

        public async Task<IEnumerable<Models.Satellite>> GetGPSConstellationAsync() =>
            await GetSatellitesAsync(SatelliteType.GlobalPositioningSystemConstellation, "gps_constellation");

        public async Task<IEnumerable<Models.Satellite>> GetGPSOperationalAsync() =>
            await GetSatellitesAsync(SatelliteType.GlobalPositioningSystemOperational, "gps_operational");

        public async Task<IEnumerable<Models.Satellite>> GetGlobalstarAsync() =>
            await GetSatellitesAsync(SatelliteType.Globalstar, "globalstar");

        public async Task<IEnumerable<Models.Satellite>> GetGlonassConstellationAsync() =>
            await GetSatellitesAsync(SatelliteType.GlonassConstellation, "glonass_constellation");

        public async Task<IEnumerable<Models.Satellite>> GetGlonassOperationalAsync() =>
            await GetSatellitesAsync(SatelliteType.GlonassOperational, "glonass_operational");

        public async Task<IEnumerable<Models.Satellite>> GetGOESAsync() =>
            await GetSatellitesAsync(SatelliteType.GOES, "goes");

        public async Task<IEnumerable<Models.Satellite>> GetGonetsAsync() =>
            await GetSatellitesAsync(SatelliteType.Gonets, "gonets");

        public async Task<IEnumerable<Models.Satellite>> GetGorizontAsync() =>
            await GetSatellitesAsync(SatelliteType.Gorizont, "gorizont");

        public async Task<IEnumerable<Models.Satellite>> GetIntelsatAsync() =>
            await GetSatellitesAsync(SatelliteType.Intelsat, "intelsat");

        public async Task<IEnumerable<Models.Satellite>> GetIRNSSAsync() =>
            await GetSatellitesAsync(SatelliteType.IRNSS, "irnss");

        public async Task<IEnumerable<Models.Satellite>> GetLemurAsync() =>
            await GetSatellitesAsync(SatelliteType.Lemur, "lemur");

        public async Task<IEnumerable<Models.Satellite>> GetMilitaryAsync() =>
            await GetSatellitesAsync(SatelliteType.Military, "military");

        public async Task<IEnumerable<Models.Satellite>> GetMolniyaAsync() =>
            await GetSatellitesAsync(SatelliteType.Molniya, "molniya");

        public async Task<IEnumerable<Models.Satellite>> GetNavyNavigationSatelliteSystemAsync() =>
            await GetSatellitesAsync(SatelliteType.NavyNavigationSatelliteSystem, "navy_navigation_satellite_system");

        public async Task<IEnumerable<Models.Satellite>> GetNOAAAsync() =>
            await GetSatellitesAsync(SatelliteType.NOAA, "noaa");

        public async Task<IEnumerable<Models.Satellite>> GetO3BNetworksAsync() =>
            await GetSatellitesAsync(SatelliteType.O3BNetworks, "o3b_networks");

        public async Task<IEnumerable<Models.Satellite>> GetOneWebAsync() =>
            await GetSatellitesAsync(SatelliteType.OneWeb, "oneweb");

        public async Task<IEnumerable<Models.Satellite>> GetOrbcommAsync() =>
            await GetSatellitesAsync(SatelliteType.Orbcomm, "orbcomm");

        public async Task<IEnumerable<Models.Satellite>> GetParusAsync() =>
            await GetSatellitesAsync(SatelliteType.Parus, "parus");

        public async Task<IEnumerable<Models.Satellite>> GetQZSSAsync() =>
            await GetSatellitesAsync(SatelliteType.QZSS, "qzss");

        public async Task<IEnumerable<Models.Satellite>> GetRadarCalibrationAsync() =>
            await GetSatellitesAsync(SatelliteType.RadarCalibration, "radar_calibration");

        public async Task<IEnumerable<Models.Satellite>> GetRadugaAsync() =>
            await GetSatellitesAsync(SatelliteType.Raduga, "raduga");

        public async Task<IEnumerable<Models.Satellite>> GetRussianLEONavigationAsync() =>
            await GetSatellitesAsync(SatelliteType.RussianLEONavigation, "russian_leo_navigation");

        public async Task<IEnumerable<Models.Satellite>> GetSatelliteBasedAugmentationSystemAsync() =>
            await GetSatellitesAsync(SatelliteType.SatelliteBasedAugmentationSystem, "satellite_based_augmentation_system");

        public async Task<IEnumerable<Models.Satellite>> GetSearchAndRescueAsync() =>
            await GetSatellitesAsync(SatelliteType.SearchAndRescue, "search_and_rescue");

        public async Task<IEnumerable<Models.Satellite>> GetSpaceAndEarthScienceAsync() =>
            await GetSatellitesAsync(SatelliteType.SpaceAndEarthScience, "space_and_earth_science");

        public async Task<IEnumerable<Models.Satellite>> GetStrelaAsync() =>
            await GetSatellitesAsync(SatelliteType.Strela, "strela");

        public async Task<IEnumerable<Models.Satellite>> GetTDRSSAsync() =>
            await GetSatellitesAsync(SatelliteType.TrackingAndDataRelaySatelliteSystem, "tdrss");

        public async Task<IEnumerable<Models.Satellite>> GetTselinaAsync() =>
            await GetSatellitesAsync(SatelliteType.Tselina, "tselina");

        public async Task<IEnumerable<Models.Satellite>> GetTsikadaAsync() =>
            await GetSatellitesAsync(SatelliteType.Tsikada, "tsikada");

        public async Task<IEnumerable<Models.Satellite>> GetTsiklonAsync() =>
            await GetSatellitesAsync(SatelliteType.Tsiklon, "tsiklon");

        public async Task<IEnumerable<Models.Satellite>> GetTVAsync() =>
            await GetSatellitesAsync(SatelliteType.TV, "tv");

        public async Task<IEnumerable<Models.Satellite>> GetWeatherAsync() =>
            await GetSatellitesAsync(SatelliteType.Weather, "weather");

        public async Task<IEnumerable<Models.Satellite>> GetWestfordNeedlesAsync() =>
            await GetSatellitesAsync(SatelliteType.WestfordNeedles, "westford_needles");

        public async Task<IEnumerable<Models.Satellite>> GetXMAndSiriusAsync() =>
            await GetSatellitesAsync(SatelliteType.XMAndSirius, "xm_and_sirius");

        public async Task<IEnumerable<Models.Satellite>> GetYaoganAsync() =>
            await GetSatellitesAsync(SatelliteType.Yaogan, "yaogan");

        async public Task<IEnumerable<Models.Satellite>> GetSatellitesAsync(SatelliteType type, string cacheKey)
        {
            var cachedData = GetSatellitesFromCache(cacheKey);
            if (cachedData != null && cachedData.Any())
            {
                return cachedData;
            }

            var response = await _nasaSatelliteClient.GetSatellitesAsync(_coords.Longitude, _coords.Latitude, 4, (int)type);

            const double proximityThresholdDegrees = 1.0;

            var satellites = response.Select(i =>
            {
                return new Models.Satellite
                {
                    Name = i.Satname,
                    Latitude = i.Satlat,
                    Longitude = i.Satlng,
                    Altitude = i.Satalt,
                    Age = i.LaunchDate
                };
            })
            .Where(s => Math.Abs(s.Latitude - _coords.Latitude) < proximityThresholdDegrees &&
                        Math.Abs(s.Longitude - _coords.Longitude) < proximityThresholdDegrees);

            CacheSatelliteData(cacheKey, satellites);

            return satellites;
        }

        public IEnumerable<Models.Satellite>? GetSatellitesFromCache(string key)
        {
            var data = _cache.Get<IEnumerable<Models.Satellite>>(key);
            return data;
        }

        public void CacheSatelliteData(string key, IEnumerable<Models.Satellite> value)
        {
            _cache.Set(key, value, DateTimeOffset.Now.Add(TimeSpan.FromMinutes(5)));
        }

    }
}
