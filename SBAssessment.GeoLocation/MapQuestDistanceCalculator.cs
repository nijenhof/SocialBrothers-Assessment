using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SBAssessment.GeoLocation.Interfaces;
using SBAssessment.GeoLocation.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SBAssessment.GeoLocation
{
    public class MapQuestDistanceCalculator : IDistanceCalculator
    {
        private const string _distanceEndpoint = "/directions/v2/route";

        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger<MapQuestDistanceCalculator> _logging;

        public MapQuestDistanceCalculator(HttpClient http, IConfiguration config, ILogger<MapQuestDistanceCalculator> logging)
        {
            _http = http;
            _config = config;
            _logging = logging;
        }

        public async Task<double> GetDistanceBetween(IAddress start, IAddress end)
        {
            string[] addresses = { start.GetAsAddressString(), end.GetAsAddressString() };
            MapQuestRequestBody requestBody = new(addresses, CreateDefaultOptions());
            HttpResponseMessage response = await _http.PostAsJsonAsync(CreateDistanceEndpoint(), requestBody);

            if (!response.IsSuccessStatusCode)
            {
                _logging.LogWarning($"API request failed", response);
                return 0;
            }

            using var responseContentStream = await response.Content.ReadAsStreamAsync();

            try
            {
                var convertedResponse = await JsonSerializer.DeserializeAsync<MapQuestResponse>(responseContentStream);
                return convertedResponse?.Route?.Distance ?? 0;
            }
            catch (JsonException e)
            {
                _logging.LogError($"Tried to parse {typeof(MapQuestResponse)} but failed.", e);
                return 0;
            }
        }

        private static MapQuestOptions CreateDefaultOptions()
        {
            return new MapQuestOptions("nl_NL", "k");
        }

        private string CreateDistanceEndpoint()
        {
            return $"{_config["MapQuestAPI:URL"]}{_distanceEndpoint}?key={_config["MapQuestAPI:Key"]}";
        }
    }
}
