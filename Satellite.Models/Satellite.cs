
using Newtonsoft.Json;

namespace Satellite.Models
{
    public class Satellite
    {
        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

}
