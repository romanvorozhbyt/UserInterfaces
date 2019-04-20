using System;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherNotifier.Models;

namespace WeatherNotifier.Helper
{
    public class Helper
    {
        private readonly HttpClient _httpClient;

        public Helper()
        {
            _httpClient = new HttpClient();

        }

        public async Task<OpenWeather> GetWeatherByCoords(double lat, double lon)
        {
            var response = _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, 
               Common.Common.APIWeatherRequest(lat, lon, "metric"))).Result;
            var resultText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var weather = JsonConvert.DeserializeObject<OpenWeather>(resultText);
                    return weather;
                }
                catch
                {
                    Debug.WriteLine(resultText);
                    return null;
                }

            }
            return null;

        }
        public async Task<OpenWeather> GetWeatherByCityName(string city, string countryCode)
        {
            var response = await _httpClient.GetAsync(Common.Common.APIWeatherRequest(city, countryCode, "metric"));
            var resultText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var weather = JsonConvert.DeserializeObject<OpenWeather>(resultText);
                    return weather;
                }
                catch
                {
                    Debug.WriteLine(resultText);
                    return null;
                }

            }
            return null;

        }

    }
}
