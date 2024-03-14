using Microsoft.EntityFrameworkCore;

namespace GZIJ20241303.Models
{
    public class GZIJ20241303DbContext : DbContext
    {
        public GZIJ20241303DbContext(DbContextOptions<GZIJ20241303DbContext> options) : base(options)
        {
        }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<DireccionesProveedor> DireccionesProveedors { get; set; }

    }
}
