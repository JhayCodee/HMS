namespace MyHostel.Application.Seguridad.Autenticacion.Commands.Dtos;

public class LoginResponseDto
{
    public string Token { get; init; } = "";
    public DateTime Expiration { get; init; }
}
