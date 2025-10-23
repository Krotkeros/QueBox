using QueBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Interfaces
{
    public interface ICapaRepository
    {
        Task<int> Add(Capa c);
        Task<bool> Update(Capa c);
        Task<bool> Delete(int Id_Capa);
    }
}
