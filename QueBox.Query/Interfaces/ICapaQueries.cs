using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface ICapaQueries
    {
        Task<Cara> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Cara>> ObtenerPorImagenDecorativaAsync(int idImagen);
        Task<IEnumerable<Cara>> ObtenerTodasAsync();
        Task<Cara> ObtenerCaraConImagenAsync(int id);
        Task<int> ObtenerNumeroPorImagenAsync(int idImagen, int numero);
        Task<IEnumerable<Cara>> ObtenerCarasDisponiblesAsync();
    }
}