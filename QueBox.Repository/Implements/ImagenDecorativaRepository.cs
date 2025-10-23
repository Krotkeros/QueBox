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
    public class ImagenDecorativaRepository : IImagenDecorativaRepository
    {
        private readonly IDbConnection _db;

        public ImagenDecorativaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(ImagenDecorativa i)
        {
            try
            {
                int Id_Img = await _db.InsertAsync(i);
                return Id_Img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int Id_Img)
        {
            try
            {
                await _db.ExecuteAsync($"DELETE FROM ImagenDecorativa WHERE Id_Img={Id_Img}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(ImagenDecorativa i)
        {
            try
            {
                return await _db.UpdateAsync(i);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
