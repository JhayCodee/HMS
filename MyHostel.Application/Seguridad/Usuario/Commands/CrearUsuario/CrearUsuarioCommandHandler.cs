namespace MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;

using AutoMapper;
using FluentValidation;
using MediatR;
using MyHostel.Application.Common.Helpers;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Domain.Interfaces.Seguridad;
using MyHostel.Domain.Entities.Security;


public class CrearUsuarioCommandHandler(IUsuarioRepository repositorio, IMapper mapper) : IRequestHandler<CrearUsuarioCommand, UsuarioDto>
{
    public async Task<UsuarioDto> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await repositorio.EmailExisteAsync(request.Email))
            throw new ValidationException("El email ya está registrado");

        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = PasswordHasher.Hash(request.Password),
            Activo = true
        };

        await repositorio.CrearAsync(usuario);

        return mapper.Map<UsuarioDto>(usuario);
    }
}
