﻿using System.Collections.Generic;
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
                SELECT Id_Capa, Id_Diseno, Numero
                FROM Capa
                WHERE Id_Capa = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Capa>(query, new { Id = id });
        }

        public async Task<IEnumerable<Capa>> ObtenerPorImagenDecorativaAsync(int id_Img)
        {
            const string query = @"
                SELECT Id_Capa, Id_Diseno, Numero
                FROM Capa
                WHERE Id_Diseno = @Id_Diseno
                ORDER BY Numero";

            return await _connection.QueryAsync<Capa>(query, new { Id_Diseno = id_Img });
        }

        public async Task<IEnumerable<Capa>> ObtenerTodasAsync()
        {
            const string query = @"
                SELECT Id_Capa, Id_Diseno, Numero
                FROM Capa
                ORDER BY Id_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }

        /*public async Task<int> ObtenerNumeroPorImagenAsync(int id_Diseno, int numero)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM Capa
                WHERE Id_Diseno = @Id_Diseno AND Numero = @Numero";

            return await _connection.ExecuteScalarAsync<int>(query, new { Id_Diseno = id_Diseno, Numero = numero });
        }*/

        public async Task<IEnumerable<Capa>> ObtenerCapasDisponiblesAsync()
        {
            const string query = @"
                SELECT c.Id_Capa, c.Id_Diseno, c.Numero
                FROM Capa c
                LEFT JOIN Diseno d ON c.Id_Capa = d.Id_Capa
                WHERE d.Id_Capa IS NULL
                ORDER BY c.Id_Capa";

            return await _connection.QueryAsync<Capa>(query);
        }

    }
}
