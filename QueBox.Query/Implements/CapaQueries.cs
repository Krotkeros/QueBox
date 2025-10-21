using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;

namespace QueBox.Query
{
    public class CaraQueries : ICapaQueries
    {
        private readonly IDbConnection _connection;

        public CaraQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Cara> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT ID_Cara, ID_IMG, Numero
                FROM Cara
                WHERE ID_Cara = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Cara>(query, new { Id = id });
        }

        public async Task<IEnumerable<Cara>> ObtenerPorImagenDecorativaAsync(int idImagen)
        {
            const string query = @"
                SELECT ID_Cara, ID_IMG, Numero
                FROM Cara
                WHERE ID_IMG = @IdImagen
                ORDER BY Numero";

            return await _connection.QueryAsync<Cara>(query, new { IdImagen = idImagen });
        }

        public async Task<IEnumerable<Cara>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT ID_Cara, ID_IMG, Numero
                FROM Cara
                ORDER BY ID_Cara";

            return await _connection.QueryAsync<Cara>(query);
        }

        public async Task<Cara> ObtenerCaraConImagenAsync(int id)
        {
            const string query = @"
                SELECT 
                    c.ID_Cara, c.ID_IMG, c.Numero,
                    i.ID_IMG, i.Url, i.Ancho, i.Alto
                FROM Cara c
                INNER JOIN Imagen_Decorativa i ON c.ID_IMG = i.ID_IMG
                WHERE c.ID_Cara = @Id";

            var caraDictionary = new Dictionary<int, Cara>();

            var result = await _connection.QueryAsync<Cara, ImagenDecorativa, Cara>(
                query,
                (cara, imagen) =>
                {
                    if (!caraDictionary.TryGetValue(cara.ID_Cara, out var caraEntry))
                    {
                        caraEntry = cara;
                        caraEntry.ImagenDecorativa = imagen;
                        caraDictionary.Add(caraEntry.ID_Cara, caraEntry);
                    }
                    return caraEntry;
                },
                new { Id = id },
                splitOn: "ID_IMG"
            );

            return result.FirstOrDefault();
        }

        public async Task<int> ObtenerNumeroPorImagenAsync(int idImagen, int numero)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Cara
                WHERE ID_IMG = @IdImagen AND Numero = @Numero";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdImagen = idImagen, Numero = numero });
        }

        public async Task<IEnumerable<Cara>> ObtenerCarasDisponiblesAsync()
        {
            const string query = @"
                SELECT c.ID_Cara, c.ID_IMG, c.Numero
                FROM Cara c
                LEFT JOIN Diseno d ON c.ID_Cara = d.ID_Cara
                WHERE d.ID_Cara IS NULL
                ORDER BY c.ID_Cara";

            return await _connection.QueryAsync<Cara>(query);
        }
    }
}