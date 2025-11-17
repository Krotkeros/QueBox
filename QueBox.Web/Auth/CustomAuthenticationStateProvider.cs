using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QueBox.Web.Auth
{
    // Esta clase hereda de AuthenticationStateProvider, que es la base para 
    // manejar el estado de autenticación en Blazor.
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // Define un ClaimsPrincipal anónimo para representar a un usuario no logeado.
        private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        /// <summary>
        /// Método requerido que Blazor llama para obtener el estado actual de la autenticación.
        /// </summary>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // En un escenario real, aquí podrías verificar una cookie o token JWT en el almacenamiento local.
            // Por ahora, siempre devolvemos un estado anónimo por defecto.
            return Task.FromResult(new AuthenticationState(anonymous));
        }

        /// <summary>
        /// Método llamado desde Login.razor cuando la autenticación es exitosa.
        /// Crea los claims (incluyendo la ID) y notifica a Blazor del cambio de estado.
        /// </summary>
        /// <param name="userId">La ID del usuario (ej: "1" para el administrador).</param>
        /// <param name="userName">El nombre del usuario.</param>
        public void MarkUserAsAuthenticated(string userId, string userName)
        {
            var claims = new[]
            {
                // *** ESTE ES EL CLAIM CLAVE ***
                // Blazor/Identity usa ClaimTypes.NameIdentifier para obtener la ID.
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var authenticatedUser = new ClaimsPrincipal(identity);

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            // Notifica a todos los componentes que escuchan el estado de autenticación (como NavMenu.razor)
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// Método llamado desde Logout.razor para cerrar la sesión.
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(anonymous));

            // Notifica a Blazor que el usuario ha regresado al estado anónimo.
            NotifyAuthenticationStateChanged(authState);
        }
    }
}