using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using MyHostel.Application.Common.Helpers;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Domain.Interfaces.Seguridad;

namespace MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;

public class CrearUsuarioCommandHandler(
    IUsuarioRepository repositorio,
    IMapper mapper,
    ILogger<CrearUsuarioCommandHandler> logger)
    : IRequestHandler<CrearUsuarioCommand, UsuarioDto>
{
    public async Task<UsuarioDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Intentando crear un nuevo usuario con email {Email}", request.Email);

        if (await repositorio.EmailExisteAsync(request.Email))
        {
            logger.LogWarning("El email {Email} ya está registrado", request.Email);

            throw new ValidationException(new List<ValidationFailure>
            {
                new ValidationFailure(nameof(request.Email), "El email ya está registrado")
            });

            //throw new ValidationException("El email ya está registrado");
        }

        var usuario = new Domain.Entities.Security.Usuario
        {
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password),
            Activo = true
        };

        await repositorio.CrearAsync(usuario);

        logger.LogInformation("Usuario creado con éxito: {UsuarioId}", usuario.Id);

        return mapper.Map<UsuarioDto>(usuario);
    }
}
