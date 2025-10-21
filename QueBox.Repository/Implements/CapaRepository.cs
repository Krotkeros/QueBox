using Dapper.Contrib.Extensions;
using QueBox.Models;
using QueBox.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using QueBox.Repository.Interfaces;

namespace QueBox.Repository.Implements
{
    public class CapaRepository : ICaparepository
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
        public async Task<int> Add(Capa C)
        {
            try
            {
                int Id_Diseno = await _db.InsertAsync(C);
                return Id_Diseno;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(Capa C)
        {
            try
            {
                int Numero = await _db.InsertAsync(C);
                return Numero;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
