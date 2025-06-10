using Microsoft.EntityFrameworkCore;
using MyHostel.Domain.Entities.Security;
using MyHostel.Domain.Interfaces.Seguridad;
using MyHostel.Infrastructure.Persistence;

namespace MyHostel.Infrastructure.Repositories.Seguridad
{
    public class UsuarioRepository(HostelDbContext context) : IUsuarioRepository
    {
        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObtenerPorIdAsync(Guid id)
        {
            return await context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }

}
