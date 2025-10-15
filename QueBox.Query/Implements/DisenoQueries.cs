using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;

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
                SELECT ID_Diseno, ID_usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE ID_Diseno = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Diseno>(query, new { Id = id });
        }

        public async Task<IEnumerable<Diseno>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            const string query = @"
                SELECT ID_Diseno, ID_usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE ID_usuario = @IdUsuario
                ORDER BY ID_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Diseno>> ObtenerPorCapaAsync(int idDiseno)
        {
            const string query = @"
                SELECT d.ID_Diseno, d.ID_usuario, d.Largo, d.Alto, d.Ancho, d.Nombre
                FROM Diseno d
                INNER JOIN Capa c ON d.ID_Diseno = c.ID_diseno
                WHERE c.ID_diseno = @IdDiseno
                ORDER BY d.ID_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { IdDiseno = idDiseno });
        }

        public async Task<IEnumerable<Diseno>> ObtenerTodosAsync()
        {
            const string query = @"
                SELECT ID_Diseno, ID_usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY ID_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query);
        }

        public async Task<Diseno> ObtenerDisenoCompletoAsync(int id)
        {
            const string query = @"
                SELECT 
                    d.ID_Diseno, d.ID_usuario, d.Largo, d.Alto, d.Ancho, d.Nombre,
                    u.ID_Usuario, u.Usuario, u.Correo,
                    c.ID_Capa, c.ID_diseno, c.Numero
                FROM Diseno d
                INNER JOIN Usuario u ON d.ID_usuario = u.ID_Usuario
                LEFT JOIN Capa c ON d.ID_Diseno = c.ID_diseno
                WHERE d.ID_Diseno = @Id
                ORDER BY c.Numero";

            var disenoDictionary = new Dictionary<int, Diseno>();

            var result = await _connection.QueryAsync<Diseno, Usuario, Capa, Diseno>(
                query,
                (diseno, usuario, capa) =>
                {
                    if (!disenoDictionary.TryGetValue(diseno.ID_Diseno, out var disenoEntry))
                    {
                        disenoEntry = diseno;
                        disenoEntry.Usuario = usuario;
                        disenoEntry.Capas = new List<Capa>();
                        disenoDictionary.Add(disenoEntry.ID_Diseno, disenoEntry);
                    }

                    if (capa != null)
                    {
                        disenoEntry.Capas.Add(capa);
                    }

                    return disenoEntry;
                },
                new { Id = id },
                splitOn: "ID_Usuario,ID_Capa"
            );

            return disenoDictionary.Values.FirstOrDefault();
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
                SELECT TOP(@Limite) ID_Diseno, ID_usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY ID_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { Limite = limite });
        }
    }
}