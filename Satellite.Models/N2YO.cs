using System.Text.Json.Serialization;

namespace Satellite.Models
{
    public class SatelliteInfo
    {
        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("above")]
        public Above[] Above { get; set; }
    }

    public class Above
    {
        [JsonPropertyName("satid")]
        public long Satid { get; set; }

        [JsonPropertyName("satname")]
        public string Satname { get; set; }

        [JsonPropertyName("intDesignator")]
        public string IntDesignator { get; set; }

        [JsonPropertyName("launchDate")]
        public DateTimeOffset LaunchDate { get; set; }

        [JsonPropertyName("satlat")]
        public double Satlat { get; set; }

        [JsonPropertyName("satlng")]
        public double Satlng { get; set; }

        [JsonPropertyName("satalt")]
        public double Satalt { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("transactionscount")]
        public long Transactionscount { get; set; }

        [JsonPropertyName("satcount")]
        public long Satcount { get; set; }
    }
}

