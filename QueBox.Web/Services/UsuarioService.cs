using QueBox.Models;
using System.Net.Http.Json;

namespace QueBox.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        private string apiUrl = "api/Usuario";

        public async Task<Usuario?> ValidateLoginAsync(string nombre, string clave)
        {

            var loginData = new { Nombre = nombre, Clave = clave };

            try
            {

                var response = await _http.PostAsJsonAsync($"{apiUrl}/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var usuarioLogeado = await response.Content.ReadFromJsonAsync<Usuario>();
                    return usuarioLogeado;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de conexión al intentar autenticar: {ex.Message}");
            }

            return null;
        }
        public async Task<List<Usuario>> GetUsuariosAsync() =>
            await _http.GetFromJsonAsync<List<Usuario>>(apiUrl) ?? [];

        public async Task<Usuario?> GetUsuarioByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Usuario>($"{apiUrl}/{id}");

        public async Task<Usuario?> CreateUsuarioAsync(Usuario usuario)
        {
            var response = await _http.PostAsJsonAsync(apiUrl, usuario);
            return await response.Content.ReadFromJsonAsync<Usuario>();
        }

        public async Task UpdateUsuarioAsync(Usuario usuario) =>
            await _http.PutAsJsonAsync($"{apiUrl}/{usuario.Id_Usuario}", usuario);

        public async Task DeleteUsuarioAsync(int id) =>
            await _http.DeleteAsync($"{apiUrl}/{id}");
    }
}