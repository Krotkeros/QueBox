using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface ICapaQueries
    {
        Task<Capa> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int idImg);
        Task<IEnumerable<Capa>> ObtenerTodasAsync();
        Task<Capa> ObtenerCapaConImagenAsync(int id);
        Task<int> ObtenerNumeroPorImagenAsync(int idImg, int numero);
        Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync();
    }
}