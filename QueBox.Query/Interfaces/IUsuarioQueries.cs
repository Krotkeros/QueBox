using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface IUsuarioQueries
    {
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task<Usuario> ObtenerPorCorreoAsync(string correo);
        Task<Usuario> ObtenerPorNombreAsync(string nombre);
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<bool> ExisteCorreoAsync(string correo);
        Task<bool> ExisteNombreAsync(string nombre);
        Task<Usuario> ValidarCredencialesAsync(string nombre, string clave);
    }
}