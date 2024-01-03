public interface ISatelliteService
{
    void CacheSatelliteData(IEnumerable<Satellite.Models.Satellite> value);
    Task<IEnumerable<Satellite.Models.Satellite>> GetSatellitesAsync();
    IEnumerable<Satellite.Models.Satellite>? GetSatellitesFromCache();
}