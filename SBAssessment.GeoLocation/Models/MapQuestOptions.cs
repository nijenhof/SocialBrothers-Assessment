using System.Text.Json.Serialization;

namespace SBAssessment.GeoLocation.Models
{
    public class MapQuestOptions
    {
        public MapQuestOptions(string locale, string unit)
        {
            Locale = locale;
            Unit = unit;
        }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

}
