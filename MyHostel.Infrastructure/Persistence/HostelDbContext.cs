using Microsoft.EntityFrameworkCore;
using MyHostel.Domain.Entities;

namespace MyHostel.Infrastructure.Persistence;

public class HostelDbContext(DbContextOptions<HostelDbContext> options) : DbContext(options)
{
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.ToTable("Habitaciones");

            entity.HasKey(h => h.Id);

            entity.Property(h => h.Nombre)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(h => h.PrecioPorNoche)
                  .HasColumnType("decimal(10,2)");

            entity.Property(h => h.FechaCreacion)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
