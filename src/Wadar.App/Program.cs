using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Wadar.App.Services;

namespace Wadar.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient());
            builder.Services.AddTransient<NOAAWeatherService>();
            builder.Services.AddTransient<GeolocationService>();
            builder.Services.AddTransient<WmkApiService>();

            await builder.Build().RunAsync();
        }
    }
}
