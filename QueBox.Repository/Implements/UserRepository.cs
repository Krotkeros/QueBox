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
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(User U)
        {
            try
            {
                int ID_usuario= await _db.InsertAsync(p);
                return ID_usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(User U)
        {
            try
            {
                var Usuario = await _db.InsertAsync(p);
                return Usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(User U)
        {
            try
            {
                var Clave = await _db.InsertAsync(p);
                return Clave;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(User U)
        {
            try
            {
                var Correo = await _db.InsertAsync(p);
                return Correo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
