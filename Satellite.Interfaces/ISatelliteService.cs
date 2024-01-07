public interface ISatelliteService
{
    Task<IEnumerable<Satellite.Models.Satellite>> GetWeatherStationsAsync();
    Task<IEnumerable<Satellite.Models.Satellite>> GetISSAsync();
    Task<IEnumerable<Satellite.Models.Satellite>> GetStarlinksAsync();
    Task<IEnumerable<Satellite.Models.Satellite>> GetIridiumsAsync();
}