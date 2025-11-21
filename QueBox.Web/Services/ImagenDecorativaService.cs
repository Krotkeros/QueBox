using QueBox.Models;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Services
{
    public class ImagenDecorativaService
    {
        private readonly HttpClient _http;

        public ImagenDecorativaService(HttpClient http)
        {
            _http = http;
        }

        private string apiUrl = "api/ImagenDecorativa";

        public async Task<List<ImagenDecorativa>> GetImagenDecorativasByDisenoAsync(int idDiseno)
        {
            return await _http.GetFromJsonAsync<List<ImagenDecorativa>>($"{apiUrl}?disenoId={idDiseno}") ?? [];
        }

        public async Task<List<ImagenDecorativa>> GetImagenDecorativasAsync() =>
            await _http.GetFromJsonAsync<List<ImagenDecorativa>>(apiUrl) ?? [];

        public async Task<ImagenDecorativa?> GetImagenDecorativaByIdAsync(int id) =>
            await _http.GetFromJsonAsync<ImagenDecorativa>($"{apiUrl}/{id}");

        public async Task<ImagenDecorativa?> CreateImagenDecorativaAsync(ImagenDecorativa imagenDecorativa)
        {
            var response = await _http.PostAsJsonAsync(apiUrl, imagenDecorativa);
            return await response.Content.ReadFromJsonAsync<ImagenDecorativa>();
        }

        public async Task UpdateImagenDecorativaAsync(ImagenDecorativa imagenDecorativa) =>
            await _http.PutAsJsonAsync($"{apiUrl}/{imagenDecorativa.Id_Img}", imagenDecorativa);

        public async Task DeleteImagenDecorativaAsync(int id) =>
            await _http.DeleteAsync($"{apiUrl}/{id}");
    }
}