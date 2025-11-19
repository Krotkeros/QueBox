using QueBox.Models;
using System.Net.Http.Json;

namespace QueBox.Services
{
    public class DisenoService
    {
        private readonly HttpClient _http;

        public DisenoService(HttpClient http)
        {
            _http = http;
        }

        private string apiUrl = "api/Diseno";

        public async Task<List<Diseno>> GetDisenosAsync(string userId)
        {
            return await _http.GetFromJsonAsync<List<Diseno>>($"{apiUrl}?userId={userId}") ?? [];
        }

        public async Task<Diseno?> GetDisenoByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Diseno>($"{apiUrl}/{id}");

        public async Task<Diseno?> CreateDisenoAsync(Diseno diseno)
        {
            var response = await _http.PostAsJsonAsync(apiUrl, diseno);
            return await response.Content.ReadFromJsonAsync<Diseno>();
        }

        public async Task UpdateDisenoAsync(Diseno diseno) =>
            await _http.PutAsJsonAsync($"{apiUrl}/{diseno.Id_Diseno}", diseno);

        public async Task DeleteDisenoAsync(int id) =>
            await _http.DeleteAsync($"{apiUrl}/{id}");
    }
}
