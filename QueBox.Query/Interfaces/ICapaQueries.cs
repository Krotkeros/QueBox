using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface ICapaQueries
    {
        Task<Capa> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int idCapa);
        Task<IEnumerable<Capa>> ObtenerTodasAsync();
        Task<Capa> ObtenerCapaConImagenAsync(int id);
        Task<int> ObtenerNumeroPorImagenAsync(int idCapa, int numero);
        Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync();
    }
}