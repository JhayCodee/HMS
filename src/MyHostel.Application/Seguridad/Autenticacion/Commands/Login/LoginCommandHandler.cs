using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MyHostel.Application.Common.Exceptions;
using MyHostel.Application.Common.Helpers;
using MyHostel.Application.Seguridad.Autenticacion.Commands.Dtos;
using MyHostel.Domain.Interfaces.Seguridad;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyHostel.Application.Seguridad.Autenticacion.Commands.Login;

/// <summary>
/// Handler para procesar el comando de login y generar un JWT si las credenciales son válidas.
/// </summary>
public class LoginCommandHandler(
    IUsuarioRepository repo,                    // Repositorio de usuarios
    IConfiguration config,                      // Configuración de appsettings.json
    ILogger<LoginCommandHandler> logger)        // Logger para registrar acciones del login
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    public async Task<LoginResponseDto> Handle(LoginCommand req, CancellationToken ct)
    {
        // Log del intento de login
        logger.LogInformation("Intento de login: {Email}", req.Email);

        // Buscar el usuario por email, si no se encuentra lanzar excepción de "no encontrado"
        var usuario = await repo.ObtenerPorEmailAsync(req.Email)
            ?? throw new NotFoundException("Usuario", req.Email);

        // Validar la contraseña usando el helper, si no coincide lanzar excepción de "no autorizado"
        if (!PasswordHasher.Verify(req.Password, usuario.PasswordHash))
        {
            logger.LogWarning("Credenciales inválidas para {Email}", req.Email);
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        // Log del éxito en la autenticación
        logger.LogInformation("Login exitoso: {Email}", req.Email);

        // Leer los valores del JWT desde el archivo de configuración
        var key = config["Jwt:Key"]!;
        var issuer = config["Jwt:Issuer"]!;
        var audience = config["Jwt:Audience"]!;
        var expire = int.Parse(config["Jwt:ExpireMinutes"]!);

        // Crear el manejador del token JWT
        var tokenHandler = new JwtSecurityTokenHandler();

        // Construir la descripción del token, incluyendo los claims del usuario
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),    // ID como subject
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email)           // Email del usuario
            ]),
            Expires = DateTime.UtcNow.AddMinutes(expire),                          // Fecha de expiración
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),            // Firma con clave simétrica
                SecurityAlgorithms.HmacSha256Signature)                           // Algoritmo de firma
        };

        // Crear el token y devolver la respuesta con token y fecha de expiración
        var token = tokenHandler.CreateToken(descriptor);
        return new LoginResponseDto
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = descriptor.Expires!.Value
        };
    }
}
