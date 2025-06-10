namespace MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;

using MediatR;
using MyHostel.Application.Seguridad.Usuario.Dtos;

public class CrearUsuarioCommand : IRequest<UsuarioDto>
{
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
