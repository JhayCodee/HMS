namespace MyHostel.Domain.Entities.Security;

public class RolPermiso
{
    public Guid RolId { get; set; }
    public Rol Rol { get; set; } = default!;

    public Guid PermisoId { get; set; }
    public Permiso Permiso { get; set; } = default!;
}
