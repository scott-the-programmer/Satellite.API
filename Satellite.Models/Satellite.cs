
using Newtonsoft.Json;

namespace Satellite.Models
{
    public class Satellite
    {
        [JsonProperty("name")]
        public required string Name { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

        [JsonProperty("longitude")]
        public float Longitude { get; set; }

    }

}
