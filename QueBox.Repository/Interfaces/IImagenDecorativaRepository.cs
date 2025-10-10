using QueBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Interfaces
{
    public interface IImagenDecorativaRepository
    {
        Task<int> Add(ImagenDecorativa I);
    }
}
