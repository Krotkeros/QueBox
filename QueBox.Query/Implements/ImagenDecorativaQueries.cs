﻿using System.Collections.Generic;
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
                SELECT Id_Img, Url, Ancho, Alto
                FROM ImagenDecorativa
                WHERE Id_Img = @Id";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Id = id });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT Id_Img, Id_Capa, Url, Ancho, Alto
                FROM ImagenDecorativa
                ORDER BY Id_Img";

            return await _connection.QueryAsync<ImagenDecorativa>(query);
        }

        public async Task<ImagenDecorativa> ObtenerPorUrlAsync(string url)
        {
            const string query = @"
                SELECT Id_Img, Id_Capa, Url, Ancho, Alto
                FROM ImagenDecorativa
                WHERE Url = @Url";

            return await _connection.QueryFirstOrDefaultAsync<ImagenDecorativa>(query, new { Url = url });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerPorDimensionesAsync(float ancho, float alto)
        {
            const string query = @"
                SELECT Id_Img, Id_Capa, Url, Ancho, Alto
                FROM ImagenDecorativa
                WHERE Ancho = @Ancho AND Alto = @Alto";

            return await _connection.QueryAsync<ImagenDecorativa>(query, new { Ancho = ancho, Alto = alto });
        }

        public async Task<IEnumerable<ImagenDecorativa>> ObtenerImagenesConCapasAsync()
        {
            const string query = @"
                SELECT 
                    i.Id_Img, i.Id_Capa, i.Url, i.Ancho, i.Alto,
                    c.Numero
                FROM ImagenDecorativa i
                LEFT JOIN Capa c ON i.Id_Capa = c.Id_Capa
                ORDER BY i.Id_Capa, c.Id_Capa";

            var imagenDictionary = new Dictionary<int, ImagenDecorativa>();

            var result = await _connection.QueryAsync<ImagenDecorativa, Capa, ImagenDecorativa>(
                query,
                (imagen, capa) =>
                {
                    if (!imagenDictionary.TryGetValue(imagen.Id_Img, out var imagenEntry))
                    {
                        imagenEntry = imagen;
                        imagenEntry.Id_Capa = capa.Id_Capa;
                        imagenDictionary.Add(imagenEntry.Id_Img, imagenEntry);
                    }

                    return imagenEntry;
                },
                splitOn: "Id_Capa"
            );

            return imagenDictionary.Values;
        }

        /*
        public async Task<int> ContarCapasPorImagenAsync(int id_Img)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Capa
                WHERE Id_Img = @Id_Img";

            return await _connection.ExecuteScalarAsync<int>(query, new { Id_Img = id_Img });
        }*/
    }
}
