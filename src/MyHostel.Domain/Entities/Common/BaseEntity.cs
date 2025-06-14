namespace MyHostel.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? CreadoPorId { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public Guid? ModificadoPorId { get; set; }
    public DateTime? FechaModificacion { get; set; }

    public bool Eliminado { get; set; } = false;
    public Guid? EliminadoPorId { get; set; }
    public DateTime? FechaEliminacion { get; set; }
}
