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
                int ID_diseno = await _db.InsertAsync(D);
                return ID_diseno;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(Diseno D)
        {
            try
            {
                int ID_usuario = await _db.InsertAsync(D);
                return ID_usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(Diseno D)
        {
            try
            {
                float Largo = await _db.InsertAsync(D);
                return Largo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(Diseno D)
        {
            try
            {
                float Alto = await _db.InsertAsync(D);
                return Alto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(Diseno D)
        {
            try
            {
                float Ancho = await _db.InsertAsync(D);
                return Ancho;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(Diseno D)
        {
            try
            {
                float Nombre = await _db.InsertAsync(D);
                return Nombre;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(Diseno D)
        {
            try
            {
                int ID_Capa = await _db.InsertAsync(D);
                return ID_Capa;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
