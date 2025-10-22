﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using QueBox.Query.Interfaces;
using QueBox.Models;


namespace QueBox.Query
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IDbConnection _connection;

        public UsuarioQueries(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            const string query = @"
                SELECT Id_Usuario, Nombre, Clave, Correo
                FROM Usuario
                WHERE Id_Usuario = @Id";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        }

        public async Task<Usuario> ObtenerPorCorreoAsync(string correo)
        {
            const string query = @"
                SELECT Id_Usuario, Nombre, Clave, Correo
                FROM Usuario
                WHERE Correo = @Correo";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Correo = correo });
        }

        public async Task<Usuario> ObtenerPorNombreAsync(string nombre)
        {
            const string query = @"
                SELECT Id_Usuario, Nombre, Clave, Correo
                FROM Usuario
                WHERE Nombre = @Nombre";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Nombre = nombre });
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            const string query = @"
                SELECT Id_Usuario, Nombre, Clave, Correo
                FROM Usuario
                ORDER BY Id_Usuario";

            return await _connection.QueryAsync<Usuario>(query);
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM Usuario
                WHERE Correo = @Correo";

            var count = await _connection.ExecuteScalarAsync<int>(query, new { Correo = correo });
            return count > 0;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            const string query = @"
                SELECT COUNT(1)
                FROM Usuario
                WHERE Nombre = @Nombre";

            var count = await _connection.ExecuteScalarAsync<int>(query, new { Nombre = nombre });
            return count > 0;
        }

        public async Task<Usuario> ValidarCredencialesAsync(string nombre, string clave)
        {
            const string query = @"
                SELECT Id_Usuario, Nombre, Clave, Correo
                FROM Usuario
                WHERE Nombre = @Nombre AND Clave = @Clave";

            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Nombre = nombre, Clave = clave });
        }
    }
}
