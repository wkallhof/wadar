using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Wadar.App.Services
{
    public class WmkApiService
    {
        private HttpClient _client { get; }

        public WmkApiService(HttpClient client)
            => _client = (client);

        public async Task<GetForecastResponse> GetForecast(string zip)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.wmk.io/weather/forecast/{zip}");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            #if DEBUG
            var body = await response.Content.ReadAsStringAsync();
            #endif

            using var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<GetForecastResponse>(stream, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public record GetForecastResponse
        {
            public List<CurrentCondition> CurrentCondition { get; set; }
            public List<NearestArea> NearestArea { get; set; }
            public List<DailyForecast> DailyForecast { get; set; }
        }

        public record CurrentCondition
        {
            public float? FeelsLikeC { get; set; }
            public float? FeelsLikeF { get; set; }
            public float? Cloudcover { get; set; }
            public float? Humidity { get; set; }
            public string LocalObservedDateTime { get; set; }
            public string ObservationTime { get; set; }
            public string PrecipitationInches { get; set; }
            public string PrecipitationMm { get; set; }
            public float? Pressure { get; set; }
            public float? PressureInches { get; set; }
            public float? TemperatureC { get; set; }
            public float? TemperatureF { get; set; }
            public float? UvIndex { get; set; }
            public float? Visibility { get; set; }
            public float? VisibilityMiles { get; set; }
            public float? WeatherCode { get; set; }
            public List<WeatherDescription> WeatherDescription { get; set; }
            public List<WeatherDescription> WeatherIconUrl { get; set; }
            public string WindDirection16Point { get; set; }
            public float? WindDirectionDegree { get; set; }
            public float? WindSpeedKmph { get; set; }
            public float? WindSpeedMiles { get; set; }
        }

        public record WeatherDescription
        {
            public string Value { get; set; }
        }

        public record NearestArea
        {
            public List<WeatherDescription> AreaName { get; set; }
            public List<WeatherDescription> Country { get; set; }
            public string Latitude { get; set; }
            public string Floatitude { get; set; }
            public float? Population { get; set; }
            public List<WeatherDescription> Region { get; set; }
            public List<WeatherDescription> WeatherUrl { get; set; }
        }

        public record DailyForecast
        {
            public List<Astronomy> Astronomy { get; set; }
            public float? AverageTemperatureC { get; set; }
            public float? AverageTemperatureF { get; set; }
            public DateTimeOffset Date { get; set; }
            public List<Hourly> Hourly { get; set; }
            public float? MaximumTemperatureC { get; set; }
            public float? MaximumTemperatureF { get; set; }
            public float? MinimumTemperatureC { get; set; }
            public float? MinimumTemperatureF { get; set; }
            public string SunHour { get; set; }
            public string TotalSnowCm { get; set; }
            public float? UvIndex { get; set; }
        }

        public record Astronomy
        {
            public float? MoonIllumination { get; set; }
            public string MoonPhase { get; set; }
            public string Moonrise { get; set; }
            public string Moonset { get; set; }
            public string Sunrise { get; set; }
            public string Sunset { get; set; }
        }

        public record Hourly
        {
            public float? DewPointC { get; set; }
            public float? DewPointF { get; set; }
            public float? FeelsLikeC { get; set; }
            public float? FeelsLikeF { get; set; }
            public float? HeatIndexC { get; set; }
            public float? HeatIndexF { get; set; }
            public float? WindChillC { get; set; }
            public float? WindChillF { get; set; }
            public float? WindGustKmph { get; set; }
            public float? WindGustMiles { get; set; }
            public float? ChanceOFfog { get; set; }
            public float? ChanceOfFrost { get; set; }
            public float? ChanceOfHighTemp { get; set; }
            public float? ChanceOfOvercast { get; set; }
            public float? ChanceOfRain { get; set; }
            public float? ChanceOfRemdry { get; set; }
            public float? ChanceOfSnow { get; set; }
            public float? ChanceOfSunshine { get; set; }
            public float? ChanceOfThunder { get; set; }
            public float? ChanceOfWindy { get; set; }
            public float? Cloudcover { get; set; }
            public float? Humidity { get; set; }
            public string PrecipitationInches { get; set; }
            public string PrecipitationMm { get; set; }
            public float? Pressure { get; set; }
            public float? PressureInches { get; set; }
            public float? TemperatureC { get; set; }
            public float? TemperatureF { get; set; }
            public float? Time { get; set; }
            public float? UvIndex { get; set; }
            public float? Visibility { get; set; }
            public float? VisibilityMiles { get; set; }
            public float? WeatherCode { get; set; }
            public List<WeatherDescription> WeatherDescription { get; set; }
            public List<WeatherDescription> WeatherIconUrl { get; set; }
            public string WindDirection16Point { get; set; }
            public float? WindDirectionDegree { get; set; }
            public float? WindSpeedKmph { get; set; }
            public float? WindSpeedMiles { get; set; }
        }
    }
}