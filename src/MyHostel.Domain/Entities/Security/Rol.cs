using MyHostel.Domain.Entities.Common;

namespace MyHostel.Domain.Entities.Security;

public class Rol : BaseEntity
{
    public string Nombre { get; set; } = default!;
    public string? Descripcion { get; set; }

    public ICollection<UsuarioRol> Usuarios { get; set; } = new List<UsuarioRol>();
    public ICollection<RolPermiso> Permisos { get; set; } = new List<RolPermiso>();
    public ICollection<RolMenuItem> Menus { get; set; } = new List<RolMenuItem>();
}
