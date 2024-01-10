
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

        [JsonProperty("altitude")]
        public float Altitude { get; set; }

        [JsonProperty("age")]
        public DateTimeOffset Age { get; set; }

    }

}
