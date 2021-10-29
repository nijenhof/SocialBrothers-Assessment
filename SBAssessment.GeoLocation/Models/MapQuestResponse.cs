using System.Text.Json.Serialization;

namespace SBAssessment.GeoLocation.Models
{
    public class MapQuestResponse
    {
        [JsonPropertyName("route")]
        public MapQuestRoute? Route { get; set; }
    }

    public class MapQuestRoute
    {
        [JsonPropertyName("distance")]
        public double Distance { get; set; }
    }
}
