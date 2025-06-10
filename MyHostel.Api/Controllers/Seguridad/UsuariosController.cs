using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;

namespace MyHostel.Api.Controllers.Seguridad;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Crear(CrearUsuarioCommand cmd)
    {
        var dto = await mediator.Send(cmd);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsuarioDto>> ObtenerPorId(Guid id)
    {
        var dto = await mediator.Send(new ObtenerUsuarioPorIdQuery(id));
        if (dto is null) return NotFound();
        return Ok(dto);
    }
}

