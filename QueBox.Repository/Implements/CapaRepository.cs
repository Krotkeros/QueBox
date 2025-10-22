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
        public async Task<int> Add(Capa C)
        {
            try
            {
                int Id_Capa = await _db.InsertAsync(C);
                return Id_Capa;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
