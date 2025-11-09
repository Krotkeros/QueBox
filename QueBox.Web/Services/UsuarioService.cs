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
