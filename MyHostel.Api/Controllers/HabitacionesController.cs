using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyHostel.Application.Habitaciones.Commands.CrearHabitacion;

namespace MyHostel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitacionesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearHabitacionCommand command)
    {
        var resultado = await mediator.Send(command);
        return CreatedAtAction(nameof(Crear), new { id = resultado.Id }, resultado);
    }
}
