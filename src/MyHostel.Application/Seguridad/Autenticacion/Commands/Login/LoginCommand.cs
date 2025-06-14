using MediatR;
using MyHostel.Application.Seguridad.Autenticacion.Commands.Dtos;

namespace MyHostel.Application.Seguridad.Autenticacion.Commands.Login;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

