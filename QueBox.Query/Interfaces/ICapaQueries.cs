using System.Collections.Generic;
using System.Threading.Tasks;
using QueBox.Models;

namespace QueBox.Query.Interfaces
{
    public interface ICapaQueries
    {
        Task<Capa> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int Id_Img);
        Task<IEnumerable<Capa>> ObtenerTodasAsync();
        Task<int> ObtenerNumeroPorImagenAsync(int Id_Img, int numero);
        Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync();

        Task<int> ActualizarCapaAsync();

    }
}