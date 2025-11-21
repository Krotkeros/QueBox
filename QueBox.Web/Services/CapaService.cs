using QueBox.Models;
using System.Net.Http.Json;
using System.Collections.Generic; 
using System.Threading.Tasks;    

namespace QueBox.Services
{
    public class CapaService
    {
        private readonly HttpClient _http;

        public CapaService(HttpClient http)
        {
            _http = http;
        }

        private string apiUrl = "api/Capa";
        public async Task<List<Capa>> GetCapasByDisenoAsync(int idDiseno)
        {
            return await _http.GetFromJsonAsync<List<Capa>>($"{apiUrl}?disenoId={idDiseno}") ?? [];
        }


        public async Task<List<Capa>> GetCapasAsync() =>
            await _http.GetFromJsonAsync<List<Capa>>(apiUrl) ?? [];

        public async Task<Capa?> GetCapaByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Capa>($"{apiUrl}/{id}");

        public async Task<Capa?> CreateCapaAsync(Capa capa)
        {
            var response = await _http.PostAsJsonAsync(apiUrl, capa);
            return await response.Content.ReadFromJsonAsync<Capa>();
        }

        public async Task UpdateCapaAsync(Capa capa) =>
            await _http.PutAsJsonAsync($"{apiUrl}/{capa.Id_Capa}", capa);

        public async Task DeleteCapaAsync(int id) =>
            await _http.DeleteAsync($"{apiUrl}/{id}");
    }
}