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
    public class DisenoRepository : IDisenoRepository
    {
        private readonly IDbConnection _db;

        public DisenoRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Diseno d)
        {
            try
            {
                int Id_Diseno = await _db.InsertAsync(d);
                return Id_Diseno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int Id_Diseno)
        {
            try
            {
                await _db.ExecuteAsync($"DELETE FROM Diseno WHERE Id_Diseno={Id_Diseno}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Diseno d)
        {
            try
            {
                return await _db.UpdateAsync(d);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
