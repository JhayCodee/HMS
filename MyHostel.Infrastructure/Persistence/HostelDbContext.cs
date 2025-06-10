using Microsoft.EntityFrameworkCore;
using MyHostel.Domain.Entities;
using MyHostel.Domain.Entities.Security;

namespace MyHostel.Infrastructure.Persistence;

public class HostelDbContext(DbContextOptions<HostelDbContext> options) : DbContext(options)
{
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();

    #region SEGURIDAD DBSETS

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Permiso> Permisos => Set<Permiso>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public DbSet<UsuarioRol> UsuarioRoles => Set<UsuarioRol>();
    public DbSet<RolPermiso> RolPermisos => Set<RolPermiso>();
    public DbSet<RolMenuItem> RolMenuItems => Set<RolMenuItem>();

    #endregion

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

        #region SEGURIDAD RELATIONSHIPS

        // Relación Usuario–Rol (N:N)
        modelBuilder.Entity<UsuarioRol>()
            .HasKey(ur => new { ur.UsuarioId, ur.RolId });

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Usuario)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UsuarioId);

        modelBuilder.Entity<UsuarioRol>()
            .HasOne(ur => ur.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(ur => ur.RolId);

        // Relación Rol–Permiso (N:N)
        modelBuilder.Entity<RolPermiso>()
            .HasKey(rp => new { rp.RolId, rp.PermisoId });

        modelBuilder.Entity<RolPermiso>()
            .HasOne(rp => rp.Rol)
            .WithMany(r => r.Permisos)
            .HasForeignKey(rp => rp.RolId);

        modelBuilder.Entity<RolPermiso>()
            .HasOne(rp => rp.Permiso)
            .WithMany(p => p.Roles)
            .HasForeignKey(rp => rp.PermisoId);

        // Relación Rol–MenuItem (N:N)
        modelBuilder.Entity<RolMenuItem>()
            .HasKey(rm => new { rm.RolId, rm.MenuItemId });

        modelBuilder.Entity<RolMenuItem>()
            .HasOne(rm => rm.Rol)
            .WithMany(r => r.Menus)
            .HasForeignKey(rm => rm.RolId);

        modelBuilder.Entity<RolMenuItem>()
            .HasOne(rm => rm.MenuItem)
            .WithMany(m => m.Roles)
            .HasForeignKey(rm => rm.MenuItemId);

        // Configuración de MenuItem hierárquico
        modelBuilder.Entity<MenuItem>()
            .HasOne(m => m.Parent)
            .WithMany(m => m.SubItems)
            .HasForeignKey(m => m.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}
