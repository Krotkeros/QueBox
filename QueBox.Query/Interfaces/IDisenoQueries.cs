using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface IDisenoQueries
    {
        Task<Diseno> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Diseno>> ObtenerPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Diseno>> ObtenerPorCapaAsync(int idCapa);
        Task<IEnumerable<Diseno>> ObtenerTodosAsync();
        Task<Diseno> ObtenerDisenoCompletoAsync(int id);
        Task<int> ContarDisenoPorUsuarioAsync(int idUsuario);
        Task<IEnumerable<Diseno>> ObtenerDisenosRecientesAsync(int limite);
    }
}