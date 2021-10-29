using System;
using System.Text.Json.Serialization;

namespace SBAssessment.GeoLocation.Models
{
    public class MapQuestRequestBody
    {
        public MapQuestRequestBody(string[] locations, MapQuestOptions options)
        {
            if (locations.Length != 2) throw new ArgumentException($"{nameof(locations)} must contain precisely 2 addresses");

            Locations = locations;
            Options = options;
        }

        [JsonPropertyName("locations")]
        public string[] Locations { get; set; }

        [JsonPropertyName("options")]
        public MapQuestOptions Options { get; set; }

    }
}
