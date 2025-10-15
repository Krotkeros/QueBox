using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;

namespace QueBox.Query
{
    public class ImagenDecorativaQueries : IImagenDecorativaQueries
    {
        private readonly IDbConnection _connection;

        public ImagenDecorativaQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<ImagenDecorativa> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT ID_IMG, ID_Capa, Ancho, Alto, Url
                FROM ImagenDecorativa
                WHERE ID_IMG = @Id";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Id = id });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT ID_IMG, ID_Capa, Ancho, Alto, Url
                FROM ImagenDecorativa
                ORDER BY ID_IMG";

            return await _connection.QueryAsync<ImagenDecorativa>(query);
        }

        public async Task<ImagenDecorativa> ObtenerPorUrlAsync(string url)
        {
            const string query = @"
                SELECT ID_IMG, ID_Capa, Ancho, Alto, Url
                FROM ImagenDecorativa
                WHERE Url = @Url";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Url = url });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerPorDimensionesAsync(float ancho, float alto)
        {
            const string query = @"
                SELECT ID_IMG, ID_Capa, Ancho, Alto, Url
                FROM ImagenDecorativa
                WHERE Ancho = @Ancho AND Alto = @Alto";

            return await _connection.QueryAsync<ImagenDecorativa>(query, new { Ancho = ancho, Alto = alto });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerImagenesConCapasAsync()
        {
            const string query = @"
                SELECT 
                    i.ID_IMG, i.ID_Capa, i.Ancho, i.Alto, i.Url,
                    c.ID_Capa, c.ID_diseno, c.Numero
                FROM ImagenDecorativa i
                INNER JOIN Capa c ON i.ID_Capa = c.ID_Capa
                ORDER BY i.ID_IMG, c.Numero";

            var imagenDictionary = new Dictionary<int, ImagenDecorativa>();

            var result = await _connection.QueryAsync<ImagenDecorativa, Capa, ImagenDecorativa>(
                query,
                (imagen, capa) =>
                {
                    if (!imagenDictionary.TryGetValue(imagen.ID_IMG, out var imagenEntry))
                    {
                        imagenEntry = imagen;
                        imagenEntry.Capa = capa;
                        imagenDictionary.Add(imagenEntry.ID_IMG, imagenEntry);
                    }

                    return imagenEntry;
                },
                splitOn: "ID_Capa"
            );

            return imagenDictionary.Values;
        }

        public async Task<int> ContarCapasPorImagenAsync(int idImagen)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM ImagenDecorativa
                WHERE ID_IMG = @IdImagen";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdImagen = idImagen });
        }
    }
}