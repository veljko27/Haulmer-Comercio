using Microsoft.EntityFrameworkCore;
using Haulmer_Comercio.Entidades;
namespace Haulmer_Comercio
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Comercios> Comercios { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
    }
}

