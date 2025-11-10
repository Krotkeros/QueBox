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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5142") });


            builder.Services.AddScoped<CapaService>();
            builder.Services.AddScoped<DisenoService>();
            builder.Services.AddScoped<ImagenDecorativaService>();
            builder.Services.AddScoped<UsuarioService>();

            await builder.Build().RunAsync();
        }
    }
}
