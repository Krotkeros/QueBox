using QueBox.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApi.Contexts
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Capa> Capas { get; set; }
        public DbSet<Diseno> Disenos { get; set; }
        public DbSet<ImagenDecorativa> ImagenDecorativas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}