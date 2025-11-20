using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QueBox.Services;
using QueBox.Web.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            var adminUserId = "1";
            builder.Services.AddSingleton<IAuthorizationPolicyProvider, DefaultAuthorizationPolicyProvider>();
            builder.Services.AddOptions<AuthorizationOptions>()
                .PostConfigure(options =>
                {
                    options.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());

                    options.AddPolicy("RequireAdminId", policy =>
                        policy.RequireAssertion(context =>
                        {
                            var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                            return context.User.Identity != null &&
                                   context.User.Identity.IsAuthenticated &&
                                   userId != null &&
                                   userId.Equals(adminUserId, StringComparison.OrdinalIgnoreCase);
                        }));
                });

            builder.Services.AddScoped<CapaService>();
            builder.Services.AddScoped<DisenoService>();
            builder.Services.AddScoped<ImagenDecorativaService>();
            builder.Services.AddScoped<UsuarioService>();

            builder.Services.AddSingleton<Mathy>();

            await builder.Build().RunAsync();
        }
    }
}