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
    public class CapaRepository : ICapaRepository
    {
        private readonly IDbConnection _db;

        public CapaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Capa c)
        {
            try
            {
                int Id_Capa = await _db.InsertAsync(c);
                return Id_Capa;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int Id_Capa)
        {
            try
            {
                await _db.ExecuteAsync($"DELETE FROM Capa WHERE Id_Capa={Id_Capa}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Capa c)
        {
            try
            {
                return await _db.UpdateAsync(c);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
