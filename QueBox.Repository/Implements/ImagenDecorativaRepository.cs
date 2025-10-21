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
    public class ImagenDecorativaRepository : IImagenDecorativaRepository
    {
        private readonly IDbConnection _db;

        public ImagenDecorativaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(ImagenDecorativa I)
        {
            try
            {
                int Id_Img = await _db.InsertAsync(p);
                return Id_Img;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(ImagenDecorativa I)
        {
            try
            {
                int Id_Capa = await _db.InsertAsync(p);
                return Id_Capa;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Add(ImagenDecorativa I)
        {
            try
            {
                var Url = await _db.InsertAsync(p);
                return Url;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(ImagenDecorativa I)
        {
            try
            {
                float Ancho = await _db.InsertAsync(p);
                return Ancho;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<float> Add(ImagenDecorativa I)
        {
            try
            {
                float Alto = await _db.InsertAsync(p);
                return Alto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
