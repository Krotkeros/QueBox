using QueBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Interfaces
{
    public interface IImagenDecorativaRepository
    {
        Task<int> Add(ImagenDecorativa i);
        Task<bool> Update(ImagenDecorativa i);
        Task<bool> Delete(int Id_Img);
    }
}
