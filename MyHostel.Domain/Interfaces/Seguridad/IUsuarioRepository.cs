using MyHostel.Domain.Entities.Security;

namespace MyHostel.Domain.Interfaces.Seguridad;

public interface IUsuarioRepository
{
    Task<Usuario> CrearAsync(Usuario usuario);
    Task<bool> EmailExisteAsync(string email);
    Task<Usuario?> ObtenerPorEmailAsync(string email);
    Task<Usuario?> ObtenerPorIdAsync(Guid id);
}
