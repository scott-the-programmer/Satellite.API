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

        async public Task<IEnumerable<Models.Satellite>> GetWeatherStationsAsync()
        {
            return await GetSatellitesAsync(SatelliteType.Weather, "weather");
        }
        async public Task<IEnumerable<Models.Satellite>> GetISSAsync()
        {
            return await GetSatellitesAsync(SatelliteType.ISS, "iss");
        }

        async public Task<IEnumerable<Models.Satellite>> GetStarlinksAsync()
        {
            return await GetSatellitesAsync(SatelliteType.Starlink, "starlink");
        }

        async public Task<IEnumerable<Models.Satellite>> GetIridiumsAsync()
        {
            return await GetSatellitesAsync(SatelliteType.Iridium, "iridium");
        }

        async public Task<IEnumerable<Models.Satellite>> GetSatellitesAsync(SatelliteType type, string cacheKey)
        {
            var cachedData = GetSatellitesFromCache(cacheKey);
            if (cachedData != null && cachedData.Any())
            {
                return cachedData;
            }

            var response = await _nasaSatelliteClient.GetSatellitesAsync(_coords.Longitude, _coords.Latitude, 90, (int)type);
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
            });

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

        private float CalculateRelativeX(float satLongitude, float currentLongitude)
        {
            return (float)((satLongitude - currentLongitude) * Math.Cos(DegreeToRadian(_coords.Latitude)));
        }

        private float CalculateRelativeY(float satLatitude, float currentLatitude)
        {
            return satLatitude - currentLatitude;
        }

        private float DegreeToRadian(double degree)
        {
            return (float)(degree * (Math.PI / 180));
        }
    }
}
