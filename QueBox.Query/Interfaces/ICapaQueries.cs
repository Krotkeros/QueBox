using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Query.Interfaces
{
    internal interface ICapaQueries
    {
    }
}


using MiApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Query.Interfaces
{
    public interface IPersonaQueries
    {
        Task<IEnumerable<Persona>> GetAll();
    }
}
