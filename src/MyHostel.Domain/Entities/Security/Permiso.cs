using MyHostel.Domain.Entities.Common;

namespace MyHostel.Domain.Entities.Security;

public class Permiso : BaseEntity
{
    public string Clave { get; set; } = default!;
    public string Descripcion { get; set; } = default!;

    public ICollection<RolPermiso> Roles { get; set; } = new List<RolPermiso>();
}
