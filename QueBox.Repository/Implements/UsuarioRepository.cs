using Dapper;
using Dapper.Contrib.Extensions;
using QueBox.Models;
using QueBox.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Implements
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _db;

        public UsuarioRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Usuario o)
        {
            try
            {
                int Id_Usuario = await _db.InsertAsync(o);
                return Id_Usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int Id_Usuario)
        {
            try
            {
                await _db.ExecuteAsync($"DELETE FROM Usuario WHERE Id_Usuario={Id_Usuario}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Usuario u)
        {
            try
            {
                return await _db.UpdateAsync(u);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
