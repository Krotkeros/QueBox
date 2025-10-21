using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;
using QueBox.Models;

namespace QueBox.Query
{
    public class DisenoQueries : IDisenoQueries
    {
        private readonly IDbConnection _connection;

        public DisenoQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Diseno> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT ID_diseno, ID_usuario, ID_Cara, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE ID_diseno = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Diseno>(query, new { Id = id });
        }

        public async Task<IEnumerable<Diseno>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            const string query = @"
                SELECT ID_diseno, ID_usuario, ID_Cara, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE ID_usuario = @IdUsuario
                ORDER BY ID_diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Diseno>> ObtenerPorCaraAsync(int idCara)
        {
            const string query = @"
                SELECT ID_diseno, ID_usuario, ID_Cara, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE ID_Cara = @IdCara
                ORDER BY ID_diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { IdCara = idCara });
        }

        public async Task<IEnumerable<Diseno>> ObtenerTodosAsync()
        {
            const string query = @"
                SELECT ID_diseno, ID_usuario, ID_Cara, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY ID_diseno DESC";

            return await _connection.QueryAsync<Diseno>(query);
        }

        public async Task<Diseno> ObtenerDisenoCompletoAsync(int id)
        {
            const string query = @"
                SELECT 
                    d.ID_diseno, d.ID_usuario, d.ID_Cara, d.Largo, d.Alto, d.Ancho, d.Nombre,
                    u.ID_usuario, u.Usuario, u.Correo,
                    c.ID_Cara, c.ID_IMG, c.Numero
                FROM Diseno d
                INNER JOIN Usuario u ON d.ID_usuario = u.ID_usuario
                INNER JOIN Cara c ON d.ID_Cara = c.ID_Cara
                WHERE d.ID_diseno = @Id";

            var disenoDictionary = new Dictionary<int, Diseno>();

            var result = await _connection.QueryAsync<Diseno, Usuario, Cara, Diseno>(
                query,
                (diseno, usuario, cara) =>
                {
                    if (!disenoDictionary.TryGetValue(diseno.ID_diseno, out var disenoEntry))
                    {
                        disenoEntry = diseno;
                        disenoEntry.Usuario = usuario;
                        disenoEntry.Cara = cara;
                        disenoDictionary.Add(disenoEntry.ID_diseno, disenoEntry);
                    }
                    return disenoEntry;
                },
                new { Id = id },
                splitOn: "ID_usuario,ID_Cara"
            );

            return result.FirstOrDefault();
        }

        public async Task<int> ContarDisenoPorUsuarioAsync(int idUsuario)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Diseno
                WHERE ID_usuario = @IdUsuario";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Diseno>> ObtenerDisenosRecientesAsync(int limite)
        {
            const string query = @"
                SELECT TOP(@Limite) ID_diseno, ID_usuario, ID_Cara, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY ID_diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { Limite = limite });
        }
    }
}