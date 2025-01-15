using Microsoft.EntityFrameworkCore;

namespace Productos.Server.Models
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }  // Agregado para la entidad Rol

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir que el campo Nombre de Producto es único
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique();

            // Definir que el campo Nombre de Rol es único
            modelBuilder.Entity<Rol>().HasIndex(r => r.Nombre).IsUnique();
        }
    }
}