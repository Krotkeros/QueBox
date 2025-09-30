using MiApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Repository.Interfaces
{
    public interface IPersonaRepository
    {
        Task<int> Add(Persona p);
    }
}
