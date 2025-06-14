using MyHostel.Domain.Entities.Common;

namespace MyHostel.Domain.Entities.Security;

public class Usuario : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public bool Activo { get; set; } = true;

    public ICollection<UsuarioRol> Roles { get; set; } = new List<UsuarioRol>();
}