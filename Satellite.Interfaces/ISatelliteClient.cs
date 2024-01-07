
using Satellite.Models;
public interface ISatelliteClient
{
    Task<IList<Above>> GetSatellitesAsync(double latitude, double longitude, int radius, int type);

}