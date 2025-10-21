using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;
using QueBox.Models;


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
                SELECT Id_Capa, Id_Img, Numero
                FROM Capa
                WHERE Id_Capa = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Capa>(query, new { Id = id });
        }

        public async Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int idImagen)
        {
            const string query = @"
                SELECT Id_Capa, Id_Img, Numero
                FROM Capa
                WHERE Id_Img = @IdImagen
                ORDER BY Numero";

            return await _connection.QueryAsync<Capa>(query, new { IdImagen = idImagen });
        }

        public async Task<IEnumerable<Capa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT Id_Capa, Id_Img, Numero
                FROM Capa
                ORDER BY Id_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }

        public async Task<Capa> ObtenerCapaConImagenAsync(int id)
        {
            const string query = @"
                SELECT 
                    c.Id_Capa, c.Id_Img, c.Numero,
                    i.Id_Img, i.Url, i.Ancho, i.Alto
                FROM Capa c
                INNER JOIN Imagen_Decorativa i ON c.Id_Img = i.Id_Img
                WHERE c.Id_Capa = @Id";

            var capaDictionary = new Dictionary<int, Capa>();

            var result = await _connection.QueryAsync<Capa, ImagenDecorativa, Capa>(
                query,
                (capa, imagen) =>
                {
                    if (!capaDictionary.TryGetValue(capa.Id_Capa, out var capaEntry))
                    {
                        capaEntry = capa;
                        capaEntry.ImagenDecorativa = imagen;
                        capaDictionary.Add(capaEntry.Id_Capa, capaEntry);
                    }
                    return capaEntry;
                },
                new { Id = id },
                splitOn: "Id_Img"
            );

            return result.FirstOrDefault();
        }

        public async Task<int> ObtenerNumeroPorImagenAsync(int idImagen, int numero)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Capa
                WHERE Id_Img = @IdImagen AND Numero = @Numero";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdImagen = idImagen, Numero = numero });
        }

        public async Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync()
        {
            const string query = @"
                SELECT c.Id_Capa, c.Id_Img, c.Numero
                FROM Capa c
                LEFT JOIN Diseno d ON c.Id_Capa = d.Id_Capa
                WHERE d.Id_Capa IS NULL
                ORDER BY c.Id_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }
    }
}