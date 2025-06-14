using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyHostel.Application.Common.Exceptions;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Domain.Interfaces.Seguridad;

namespace MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;

/// <summary>
/// Manejador de la consulta para obtener un usuario por ID.
/// </summary>
public class ObtenerUsuarioPorIdQueryHandler(
    IUsuarioRepository repositorio,
    IMapper mapper,
    ILogger<ObtenerUsuarioPorIdQueryHandler> logger)
    : IRequestHandler<ObtenerUsuarioPorIdQuery, UsuarioDto>
{
    public async Task<UsuarioDto> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Buscando usuario con ID {Id}", request.Id);

        var usuario = await repositorio.ObtenerPorIdAsync(request.Id);
        if (usuario is null)
        {
            logger.LogWarning("No se encontró el usuario con ID {Id}", request.Id);
            throw new NotFoundException("Usuario", request.Id);
        }

        var dto = mapper.Map<UsuarioDto>(usuario);

        logger.LogInformation("Usuario encontrado: {Id}", dto.Id);

        return dto;
    }
}
