using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace QueBox.Web.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime; 
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        // Constructor para recibir la inyección de IJSRuntime
        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Método requerido que Blazor llama para obtener el estado actual de la autenticación.
        /// Verifica si hay claims (ID de usuario) guardados en LocalStorage.
        /// </summary>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 1. Intentar leer la ID del usuario desde LocalStorage
            var userId = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userId");
            var userName = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userName");

            if (string.IsNullOrEmpty(userId))
            {
                // Si no hay datos (la sesión se perdió o nunca existió), devuelve anónimo
                return new AuthenticationState(anonymous);
            }
            else
            {
                // Si hay datos, construye la identidad autenticada
                var identity = new ClaimsIdentity(new[]
                {
                    // Debemos usar ClaimTypes.NameIdentifier para que la política "RequireAdminId" funcione
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }, "CustomAuth");

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }

        /// <summary>
        /// Marca al usuario como autenticado, guarda los claims en LocalStorage y notifica a Blazor.
        /// </summary>
        public void MarkUserAsAuthenticated(string userId, string userName)
        {
            // 1. GUARDAR DATOS en LocalStorage
            _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", userId);
            _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userName", userName);

            // 2. Construir los Claims y notificar a Blazor
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var authenticatedUser = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// Cierra la sesión, elimina los datos de LocalStorage y notifica a Blazor.
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            //ELIMINAR DATOS del LocalStorage (Limpiar sesión)
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userId");
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userName");

            //Notificar a Blazor que el usuario es anónimo
            var authState = Task.FromResult(new AuthenticationState(anonymous));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}