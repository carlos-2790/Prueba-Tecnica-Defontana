using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Prueba_Defontana
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=database")
        {
        }

        public virtual DbSet<Local> Local { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Local>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Local>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Local>()
                .HasMany(e => e.Venta)
                .WithRequired(e => e.Local)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .HasMany(e => e.Producto)
                .WithRequired(e => e.Marca)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.VentaDetalle)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venta>()
                .HasMany(e => e.VentaDetalle)
                .WithRequired(e => e.Venta)
                .WillCascadeOnDelete(false);
        }
    }
}
