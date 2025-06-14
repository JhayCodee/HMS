using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;
using MyHostel.Api.Responses;

namespace MyHostel.Api.Controllers.Seguridad;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    /// <returns>El usuario creado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UsuarioDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<UsuarioDto>>> Crear(CrearUsuarioCommand cmd)
    {
        var dto = await mediator.Send(cmd);
        var response = new ApiResponse<UsuarioDto>(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, response);
    }

    /// <summary>
    /// Obtiene un usuario por su ID
    /// </summary>
    /// <param name="id">Identificador único del usuario</param>
    /// <returns>El usuario si existe</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<UsuarioDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<UsuarioDto>>> ObtenerPorId(Guid id)
    {
        var dto = await mediator.Send(new ObtenerUsuarioPorIdQuery(id));
        if (dto is null)
            return NotFound(new ApiErrorResponse(404, "Usuario no encontrado", "No se encontró el usuario con el ID proporcionado"));

        return Ok(new ApiResponse<UsuarioDto>(dto));
    }
}
