using System.Collections.Generic;
using System.Threading.Tasks;
using QueBox.Models;


namespace QueBox.Query.Interfaces
{
    public interface IImagenDecorativaQueries
    {
        Task<ImagenDecorativa> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ImagenDecorativa>> ObtenerTodasAsync();
        Task<ImagenDecorativa> ObtenerPorUrlAsync(string url);
        Task<IEnumerable<ImagenDecorativa>> ObtenerPorDimensionesAsync(float largo, float alto);
        Task<IEnumerable<ImagenDecorativa>> ObtenerImagenesConCarasAsync();
        Task<int> ContarCarasPorImagenAsync(int idImagen);
    }
}