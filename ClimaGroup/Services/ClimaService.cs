using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClimaGroup.Models;

namespace ClimaGroup.Services
{
    public class ClimaService
    {
        private const string baseUrl = "https://api.open-meteo.com/v1/forecast";

        public async Task<ClimaData?> ObtenerClimaAsync(double lat, double lon)
        {
            using var httpClient = new HttpClient();
            string url = $"{baseUrl}?latitude={lat}&longitude={lon}&current=temperature_2m,relative_humidity_2m,rain";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var current = root.GetProperty("current");

            return new ClimaData
            {
                Hora = DateTime.Parse(current.GetProperty("time").GetString() ?? ""),
                Temperatura = current.GetProperty("temperature_2m").GetDouble(),
                Humedad = current.GetProperty("relative_humidity_2m").GetInt32(),
                Lluvia = current.GetProperty("rain").GetDouble()
            };
        }
    }
}
