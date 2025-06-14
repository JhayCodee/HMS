using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyHostel.Api.Responses;
using MyHostel.Application.Seguridad.Autenticacion.Commands.Dtos;
using MyHostel.Application.Seguridad.Autenticacion.Commands.Login;

namespace MyHostel.Api.Controllers.Seguridad;

[ApiController]
[Route("api/[controller]")]
public class AutenticacionController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Inicia sesión con las credenciales del usuario y devuelve un token JWT.
    /// </summary>
    /// <param name="cmd">Comando de login con email y contraseña</param>
    /// <returns>Token JWT y tiempo de expiración</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(LoginCommand cmd)
    {
        var dto = await mediator.Send(cmd);
        return Ok(new ApiResponse<LoginResponseDto>(dto));
    }
}