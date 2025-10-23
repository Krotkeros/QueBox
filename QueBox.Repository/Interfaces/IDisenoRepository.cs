using QueBox.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Interfaces
{
    public interface IDisenoRepository
    {
        Task<int> Add(Diseno d);
        Task<bool> Update(Diseno d);
        Task<bool> Delete(int Id_Diseno);
    }
}

