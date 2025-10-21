using System.Collections.Generic;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    public interface IUsuarioQueries
    {
        Task<Usuario> ObtenerPorIdAsync(int id);
        Task<Usuario> ObtenerPorCorreoAsync(string correo);
        Task<Usuario> ObtenerPorUsuarioAsync(string usuario);
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<bool> ExisteCorreoAsync(string correo);
        Task<bool> ExisteUsuarioAsync(string usuario);
        Task<Usuario> ValidarCredencialesAsync(string usuario, string clave);
    }
}