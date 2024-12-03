using CustomerManagementApp.Models;
using System.Text.Json;

namespace CustomerManagementApp.Services
{
    public class AutosuggestService
    {
        private static readonly string BingMapsApiKey = "YOUR_BING_MAPS_API_KEY";
        //private static readonly string BingAutosuggestUrl = "https://dev.virtualearth.net/REST/v1/Autosuggest";
        //string requestUrl = $"{BingAutosuggestUrl}?q={Uri.EscapeDataString(query)}&key={BingMapsApiKey}";

        private readonly HttpClient _httpClient;

        public AutosuggestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetSuggestion(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"REST/v1/Autosuggest?q{Uri.EscapeDataString(query)}&key={BingMapsApiKey}");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var autosuggestResponse = JsonSerializer.Deserialize<BingAutosuggestResponse>(jsonResponse);
                return autosuggestResponse?.ResourceSets?[0]?.Resources?[0]?.Suggestions.ToList();
            }
            catch (Exception ex)
            {
                var x = ex;
                return new List<string>();
            }
        }
    }
}
