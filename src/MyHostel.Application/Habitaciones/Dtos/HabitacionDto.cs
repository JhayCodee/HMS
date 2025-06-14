namespace MyHostel.Application.Habitaciones.Dtos;

public class HabitacionDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = default!;
    public int Capacidad { get; set; }
    public string Tipo { get; set; } = default!;
    public string Estado { get; set; } = default!;
    public decimal PrecioPorNoche { get; set; }
    public DateTime FechaCreacion { get; set; }
}
