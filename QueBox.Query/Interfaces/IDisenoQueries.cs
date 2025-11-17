using System.Collections.Generic;
using System.Threading.Tasks;
using QueBox.Models;


namespace QueBox.Query.Interfaces
{
    public interface IDisenoQueries
    {
        Task<Diseno> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Diseno>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Diseno>> ObtenerTodosAsync();
        Task<Diseno> ObtenerDisenoCompletoAsync(int id);
        Task<int> ContarDisenoPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Diseno>> ObtenerDisenosRecientesAsync(int limite);
    }
}