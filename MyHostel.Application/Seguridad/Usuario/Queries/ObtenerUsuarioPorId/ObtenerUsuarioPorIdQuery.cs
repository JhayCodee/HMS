using MediatR;
using MyHostel.Application.Seguridad.Usuario.Dtos;

namespace MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;

/// <summary>
/// Query para obtener un usuario por su identificador único.
/// </summary>
public class ObtenerUsuarioPorIdQuery(Guid id) : IRequest<UsuarioDto>
{
    public Guid Id { get; set; } = id;
}
