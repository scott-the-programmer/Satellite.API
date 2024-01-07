
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

        [JsonProperty("relativeX")]
        public float RelativeX { get; set; }

        [JsonProperty("relativeY")]
        public float RelativeY { get; set; }
    }

}
