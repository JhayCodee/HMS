using MediatR;
using MyHostel.Application.Habitaciones.Dtos;

namespace MyHostel.Application.Habitaciones.Commands.CrearHabitacion;

public class CrearHabitacionCommand : IRequest<HabitacionDto>
{
    public string Nombre { get; set; } = default!;
    public int Capacidad { get; set; }
    public int Tipo { get; set; }
    public int Estado { get; set; }
    public decimal PrecioPorNoche { get; set; }
}
