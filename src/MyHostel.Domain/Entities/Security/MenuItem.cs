using MyHostel.Domain.Entities.Common;

namespace MyHostel.Domain.Entities.Security;
public class MenuItem : BaseEntity
{
    public string Titulo { get; set; } = default!;
    public string Ruta { get; set; } = default!;
    public string? Icono { get; set; }
    public int Orden { get; set; }

    public Guid? ParentId { get; set; }
    public MenuItem? Parent { get; set; }
    public ICollection<MenuItem> SubItems { get; set; } = new List<MenuItem>();

    public ICollection<RolMenuItem> Roles { get; set; } = new List<RolMenuItem>();
}