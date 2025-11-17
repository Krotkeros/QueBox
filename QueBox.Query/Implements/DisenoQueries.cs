﻿using System.Collections.Generic;
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
                SELECT Id_Diseno, Id_Usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE Id_Diseno = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Diseno>(query, new { Id = id });
        }

        public async Task<IEnumerable<Diseno>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            const string query = @"
                SELECT Id_Diseno, Id_Usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                WHERE Id_Usuario = @IdUsuario
                ORDER BY Id_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Diseno>> ObtenerTodosAsync()
        {
            const string query = @"
                SELECT Id_Diseno, Id_Usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY Id_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query);
        }

        public async Task<Diseno> ObtenerDisenoCompletoAsync(int id)
        {
            const string query = @"
                SELECT 
                    d.Id_Diseno, d.Id_Usuario, d.Largo, d.Alto, d.Ancho, d.Nombre,
                    u.Id_Usuario, u.Nombre, u.Correo
                FROM Diseno d
                INNER JOIN Usuario u ON d.Id_Usuario = u.Id_Usuario
                WHERE d.Id_Diseno = @Id";

            var disenoDictionary = new Dictionary<int, Diseno>();

            var result = await _connection.QueryAsync<Diseno, Usuario, Diseno>(
                query,
                (diseno, usuario) =>
                {
                    if (!disenoDictionary.TryGetValue(diseno.Id_Diseno, out var disenoEntry))
                    {
                        disenoEntry = diseno;
                        disenoEntry.Id_Usuario = usuario.Id_Usuario;
                        disenoDictionary.Add(disenoEntry.Id_Diseno, disenoEntry);
                    }
                    return disenoEntry;
                },
                new { Id = id },
                splitOn: "Id_Usuario"
            );

            return result.FirstOrDefault();
        }

        public async Task<int> ContarDisenoPorUsuarioAsync(int idUsuario)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Diseno
                WHERE Id_Usuario = @IdUsuario";

            return await _connection.ExecuteScalarAsync<int>(query, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Diseno>> ObtenerDisenosRecientesAsync(int limite)
        {
            const string query = @"
                SELECT TOP(@Limite) Id_Diseno, Id_Usuario, Largo, Alto, Ancho, Nombre
                FROM Diseno
                ORDER BY Id_Diseno DESC";

            return await _connection.QueryAsync<Diseno>(query, new { Limite = limite });
        }
    }
}