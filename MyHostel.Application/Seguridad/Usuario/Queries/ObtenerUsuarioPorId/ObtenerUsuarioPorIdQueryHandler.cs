namespace MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;

using AutoMapper;
using MediatR;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Domain.Interfaces.Seguridad;


public class ObtenerUsuarioPorIdQueryHandler(IUsuarioRepository repositorio, IMapper mapper) : IRequestHandler<ObtenerUsuarioPorIdQuery, UsuarioDto?>
{
    public async Task<UsuarioDto?> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await repositorio.ObtenerPorIdAsync(request.Id);
        if (usuario is null) return null;
        return mapper.Map<UsuarioDto>(usuario);
    }
}