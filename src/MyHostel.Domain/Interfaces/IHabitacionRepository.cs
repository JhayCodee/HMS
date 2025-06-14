using MyHostel.Domain.Entities;

namespace MyHostel.Domain.Interfaces;

public interface IHabitacionRepository
{
    Task<List<Habitacion>> ObtenerTodasAsync();
    Task<Habitacion?> ObtenerPorIdAsync(Guid id);
    Task CrearAsync(Habitacion habitacion);
}
