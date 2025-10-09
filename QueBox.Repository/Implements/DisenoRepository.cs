using MiApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QueBox.Repository.Implements
{
    internal class DisenoRepository
    {
    }
}

//

using Dapper.Contrib.Extensions;
using MiApi.Models;
using MiApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Repository.Implements
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly IDbConnection _db;

        public PersonaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Persona p)
        {
            try
            {
                var id = await _db.InsertAsync(p);
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
