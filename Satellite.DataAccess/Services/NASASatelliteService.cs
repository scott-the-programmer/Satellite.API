using Microsoft.Extensions.Caching.Memory;
using Satellite.Models;
namespace Satellite.DataAccess.Services
{
    public class SatelliteService : ISatelliteService
    {
        private readonly ISatelliteClient _nasaSatelliteClient;
        private readonly IMemoryCache _cache;
        private readonly CurrentCoords _coords;
        private readonly string _cacheKey;

        public SatelliteService(ISatelliteClient nasaSatelliteClient, IMemoryCache cache, CurrentCoords coords, string cacheKey = "the_satellite_bin")
        {
            this._nasaSatelliteClient = nasaSatelliteClient;
            this._cache = cache;
            this._cacheKey = cacheKey;
            this._coords = coords;
        }

        async public Task<IEnumerable<Models.Satellite>> GetSatellitesAsync()
        {
            var cachedData = GetSatellitesFromCache();
            if (cachedData != null && cachedData.Any())
            {
                return cachedData;
            }

            var response = await _nasaSatelliteClient.GetSatellitesAsync(_coords.Longitude, _coords.Latitude, 90);
            var satellites = response.Select(i => new Models.Satellite
            {
                Name = i.Satname,
                Latitude = i.Satlat,
                Longitude = i.Satlng,
            });

            CacheSatelliteData(satellites);

            return satellites;
        }

        public IEnumerable<Models.Satellite>? GetSatellitesFromCache()
        {
            var data = _cache.Get<IEnumerable<Models.Satellite>>(_cacheKey);
            return data;
        }

        public void CacheSatelliteData(IEnumerable<Models.Satellite> value)
        {
            _cache.Set(_cacheKey, value, DateTimeOffset.Now.Add(TimeSpan.FromMinutes(5)));
        }
    }
}
