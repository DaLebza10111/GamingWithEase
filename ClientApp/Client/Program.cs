using ClientApp;
using ClientApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7257/") });

            builder.Services.AddBlazorBootstrap();

            builder.Services.AddHttpClient<BackEndHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7257/");
            });

            await builder.Build().RunAsync();
        }
    }
}
