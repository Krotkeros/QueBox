using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;

namespace QueBox.Query
{
    public class CapaQueries : ICapaQueries
    {
        private readonly IDbConnection _connection;

        public CapaQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Capa> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT ID_Capa, ID_diseno, Numero
                FROM Capa
                WHERE ID_Capa = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Capa>(query, new { Id = id });
        }

        public async Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int idCapa)
        {
            const string query = @"
                SELECT c.ID_Capa, c.ID_diseno, c.Numero
                FROM Capa c
                INNER JOIN ImagenDecorativa i ON c.ID_Capa = i.ID_Capa
                WHERE c.ID_Capa = @IdCapa
                ORDER BY c.Numero";

            return await _connection.QueryAsync<Capa>(query, new { IdCapa = idCapa });
        }

        public async Task<IEnumerable<Capa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT ID_Capa, ID_diseno, Numero
                FROM Capa
                ORDER BY ID_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }

        public async Task<Capa> ObtenerCapaConImagenAsync(int id)
        {
            const string query = @"
                SELECT 
                    c.ID_Capa, c.ID_diseno, c.Numero,
                    i.ID_IMG, i.ID_Capa, i.Ancho, i.Alto, i.Url
                FROM Capa c
                LEFT JOIN ImagenDecorativa i ON c.ID_Capa = i.ID_Capa
                WHERE c.ID_Capa = @Id";

            var capaDictionary = new Dictionary<int, Capa>();

            var result = await _connection.QueryAsync<Capa, ImagenDecorativa, Capa>(
                query,
                (capa, imagen) =>
                {
                    if (!capaDictionary.TryGetValue(capa.ID_Capa, out var capaEntry))
                    {
                        capaEntry = capa;
                        capaEntry.ImagenesDecorativas = new List<ImagenDecorativa>();
                        capaDictionary.Add(capaEntry.ID_Capa, capaEntry);
                    }

                    if (imagen != null)
                    {
                        capaEntry.ImagenesDecorativas.Add(imagen);
                    }

                    return capaEntry;
                },
                new { Id = id },
                splitOn: "ID_IMG"
            );

            return capaDictionary.Values.FirstOrDefault();
        }

        public async Task<int> ObtenerNumeroPorImagenAsync(int idCapa, int numero)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Capa
                WHERE ID_Capa = @IdCapa AND Numero = @Numero";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdCapa = idCapa, Numero = numero });
        }

        public async Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync()
        {
            const string query = @"
                SELECT c.ID_Capa, c.ID_diseno, c.Numero
                FROM Capa c
                LEFT JOIN ImagenDecorativa i ON c.ID_Capa = i.ID_Capa
                WHERE i.ID_Capa IS NULL
                ORDER BY c.ID_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }
    }
}