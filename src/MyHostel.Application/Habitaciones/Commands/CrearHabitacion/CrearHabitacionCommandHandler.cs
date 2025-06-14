using AutoMapper;
using MediatR;
using MyHostel.Application.Habitaciones.Dtos;
using MyHostel.Domain.Interfaces;

namespace MyHostel.Application.Habitaciones.Commands.CrearHabitacion;

public class CrearHabitacionCommandHandler(IHabitacionRepository repositorio, IMapper mapper) : IRequestHandler<CrearHabitacionCommand, HabitacionDto>
{
    private readonly IHabitacionRepository _repositorio = repositorio;
    private readonly IMapper _mapper = mapper;

    public async Task<HabitacionDto> Handle(CrearHabitacionCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Habitacion>(request);
        await _repositorio.CrearAsync(entity);
        return _mapper.Map<HabitacionDto>(entity);
    }
}
