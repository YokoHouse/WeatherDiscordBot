using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordWeatherBot
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherData> GetWeatherAsync()
        {
            var apiKey = "AAPI KEY ";  
            var url = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q=Sofia&aqi=no"; 

            var response = await _httpClient.GetStringAsync(url);
            var weatherData = JsonConvert.DeserializeObject<WeatherData>(response);

            return weatherData;
        }
    }

    public class WeatherData
    {
        [JsonProperty("current")]
        public CurrentWeather Current { get; set; }
    }

    public class CurrentWeather
    {
        [JsonProperty("temp_c")]
        public double TempC { get; set; }

        [JsonProperty("feelslike_c")]
        public double FeelsLikeC { get; set; }

        [JsonProperty("condition")]
        public Condition Condition { get; set; }
    }

    public class Condition
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}