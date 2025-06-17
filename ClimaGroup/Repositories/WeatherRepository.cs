using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using ClimaGroup.Models;
using ClimaGroup.Repositories;

namespace ClimaGroup.Repositories
{
    class WeatherRepository
    {
        public async Task<WeatherData> GetWeatherCurrentLocationAsync()
        {
            GeolocationRepository geolocationRepository = new GeolocationRepository();
            var location = await geolocationRepository.GetCurrentLocation();
            return await GetWeatherDataAsync(location.Latitude, location.Longitude);
        }

        public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude)
        {
            HttpClient httpClient = new HttpClient();
            string url = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude;

            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            WeatherData data = JsonConvert.DeserializeObject<WeatherData>(result);
            return data;
        }
    }

}
