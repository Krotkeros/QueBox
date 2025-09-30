using MiApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Contexts
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
    }
}
