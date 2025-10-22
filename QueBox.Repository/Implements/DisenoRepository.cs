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
        public async Task<int> Add(Diseno D)
        {
            try
            {
                int Id_Diseno = await _db.InsertAsync(D);
                return Id_Diseno;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
