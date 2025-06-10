
namespace MyHostel.Domain.Entities.Security;

public class RolMenuItem
{
    public Guid RolId { get; set; }
    public Rol Rol { get; set; } = default!;

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
}
