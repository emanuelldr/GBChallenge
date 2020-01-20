using GBChallenge.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GBChallenge.API.Data.DataContext
{
    public class DBRevendedorContext : IdentityDbContext
    {
        public DBRevendedorContext(DbContextOptions<DBRevendedorContext> options) : base(options) { }

        public virtual DbSet<Revendedor> Revendedores { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Revendedor>( entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                entity.Property(e => e.CPF)
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsRequired();

            });
            modelBuilder.Entity<Revendedor>().ToTable("Revendedor");


            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.Valor)
                    .IsRequired();

                entity.Property(e => e.Data)
                    .IsRequired();

                entity.Property(e => e.IdRevendedor)
                    .IsRequired();

            });

            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .HasOne(p => p.Revendedor)
                .WithMany(b => b.Compras)
                .HasForeignKey(p => p.IdRevendedor);

            base.OnModelCreating(modelBuilder);
        }
    }
}
