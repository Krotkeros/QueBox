

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;

namespace QueBox.Query
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IDbConnection _connection;

        public UsuarioQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT ID_usuario, Usuario, Clave, Correo
                FROM Usuario
                WHERE ID_usuario = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        }

        public async Task<Usuario> ObtenerPorCorreoAsync(string correo)
        {
            const string query = @"
                SELECT ID_usuario, Usuario, Clave, Correo
                FROM Usuario
                WHERE Correo = @Correo";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Correo = correo });
        }

        public async Task<Usuario> ObtenerPorUsuarioAsync(string usuario)
        {
            const string query = @"
                SELECT ID_usuario, Usuario, Clave, Correo
                FROM Usuario
                WHERE Usuario = @Usuario";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Usuario = usuario });
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            const string query = @"
                SELECT ID_usuario, Usuario, Clave, Correo
                FROM Usuario
                ORDER BY ID_usuario";

            return await _connection.QueryAsync<Usuario>(query);
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM Usuario
                WHERE Correo = @Correo";

            var count = await _connection.ExecuteScalarAsync<int>(query, new { Correo = correo });
            return count > 0;
        }

        public async Task<bool> ExisteUsuarioAsync(string usuario)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM Usuario
                WHERE Usuario = @Usuario";

            var count = await _connection.ExecuteScalarAsync<int>(query, new { Usuario = usuario });
            return count > 0;
        }

        public async Task<Usuario> ValidarCredencialesAsync(string usuario, string clave)
        {
            const string query = @"
                SELECT ID_usuario, Usuario, Clave, Correo
                FROM Usuario
                WHERE Usuario = @Usuario AND Clave = @Clave";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Usuario = usuario, Clave = clave });
        }
    }
}