using QueBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> Add(Usuario U);
        Task<bool> Update(Usuario u);
        Task<bool> Delete(int Id_Usuario);
    }
}
