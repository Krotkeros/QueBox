
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;
using QueBox.Models;


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
                SELECT ID_IMG, Url, Ancho, Alto
                FROM Imagen_Decorativa
                WHERE ID_IMG = @Id";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Id = id });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT ID_IMG, Url, Ancho, Alto
                FROM Imagen_Decorativa
                ORDER BY ID_IMG";

            return await _connection.QueryAsync<ImagenDecorativa>(query);
        }

        public async Task<ImagenDecorativa> ObtenerPorUrlAsync(string url)
        {
            const string query = @"
                SELECT ID_IMG, Url, Ancho, Alto
                FROM Imagen_Decorativa
                WHERE Url = @Url";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Url = url });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerPorDimensionesAsync(float largo, float alto)
        {
            const string query = @"
                SELECT ID_IMG, Url, Ancho, Alto
                FROM Imagen_Decorativa
                WHERE Ancho = @Largo AND Alto = @Alto";

            return await _connection.QueryAsync<ImagenDecorativa>(query, new { Largo = largo, Alto = alto });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerImagenesConCarasAsync()
        {
            const string query = @"
                SELECT 
                    i.ID_IMG, i.Url, i.Ancho, i.Alto,
                    c.ID_Cara, c.ID_IMG, c.Numero
                FROM Imagen_Decorativa i
                LEFT JOIN Cara c ON i.ID_IMG = c.ID_IMG
                ORDER BY i.ID_IMG, c.Numero";

            var imagenDictionary = new Dictionary<int, ImagenDecorativa>();

            var result = await _connection.QueryAsync<ImagenDecorativa, Cara, ImagenDecorativa>(
                query,
                (imagen, cara) =>
                {
                    if (!imagenDictionary.TryGetValue(imagen.ID_IMG, out var imagenEntry))
                    {
                        imagenEntry = imagen;
                        imagenEntry.Caras = new List<Cara>();
                        imagenDictionary.Add(imagenEntry.ID_IMG, imagenEntry);
                    }

                    if (cara != null)
                    {
                        imagenEntry.Caras.Add(cara);
                    }

                    return imagenEntry;
                },
                splitOn: "ID_Cara"
            );

            return imagenDictionary.Values;
        }

        public async Task<int> ContarCarasPorImagenAsync(int idImagen)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Cara
                WHERE ID_IMG = @IdImagen";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdImagen = idImagen });
        }
    }
}