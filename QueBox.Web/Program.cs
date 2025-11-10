using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QueBox.Services;

namespace QueBox.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
                
            var apiBaseAddress = "https://localhost:7117/";
            builder.Services.AddScoped(sp => new HttpClient 
            { 
                BaseAddress = new Uri(apiBaseAddress) 
            });

            // Registra los servicios
            builder.Services.AddScoped<DisenoService>();
            builder.Services.AddScoped<CapaService>();
            builder.Services.AddScoped<ImagenDecorativaService>();
            builder.Services.AddScoped<UsuarioService>();

            await builder.Build().RunAsync();
        }
    }
}