using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wadar.App.Services
{
    public class GeolocationService
    {
        public HttpClient _client { get; }

        public GeolocationService(HttpClient client)
            => (_client) = (client);

        public async Task<Location> GetLatLongByZip(string zip)
        {
            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"https://api.promaptools.com/service/us/zip-lat-lng/get/?zip={zip}&key=17o8dysaCDrgv1c"));
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<GetZipLatLongResponse>(stream, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Output?.FirstOrDefault();
        }

        public record GetZipLatLongResponse(int Status, List<Location> Output);

        public record Location(string Zip, string Latitude, string Longitude);
    }
}