using Microsoft.EntityFrameworkCore;
using MyHostel.Domain.Entities;
using MyHostel.Domain.Interfaces;
using MyHostel.Infrastructure.Persistence;

namespace MyHostel.Infrastructure.Repositories;

public class HabitacionRepository(HostelDbContext context) : IHabitacionRepository
{
    private readonly HostelDbContext _context = context;

    public async Task<List<Habitacion>> ObtenerTodasAsync()
    {
        return await _context.Habitaciones.ToListAsync();
    }

    public async Task<Habitacion?> ObtenerPorIdAsync(Guid id)
    {
        return await _context.Habitaciones.FindAsync(id);
    }

    public async Task CrearAsync(Habitacion habitacion)
    {
        _context.Habitaciones.Add(habitacion);
        await _context.SaveChangesAsync();
    }
}
