namespace MyHostel.Domain.Entities;

public class Habitacion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nombre { get; set; } = default!;
    public int Capacidad { get; set; }

    public TipoHabitacion Tipo { get; set; }
    public EstadoHabitacion Estado { get; set; } = EstadoHabitacion.Disponible;

    public decimal PrecioPorNoche { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
