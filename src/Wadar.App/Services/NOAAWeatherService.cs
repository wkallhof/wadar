using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wadar.App.Services
{
    public class NOAAWeatherService
    {
        public HttpClient _client { get; }

        public NOAAWeatherService(HttpClient client)
            => (_client) = (client);

        public async Task<List<DateTimeOffset>> GetTimeStops()
        {
            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://nowcoast.noaa.gov/layerinfo?request=timestops&service=radar_meteo_imagery_nexrad_time&layers=1&format=json"));
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<GetTimeStopsResponse>(stream, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Layers.FirstOrDefault()?.TimeStops.Select(x => DateTimeOffset.FromUnixTimeMilliseconds(x)).ToList() ?? new List<DateTimeOffset>();
        }

        public record GetTimeStopsResponse(string Service, List<GetTimeStopsLayer> Layers);

        public record GetTimeStopsLayer(string Id, string Name, List<long> TimeStops);
    }
}