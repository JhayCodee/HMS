namespace MyHostel.Application.Seguridad.Usuario.Queries.ObtenerUsuarioPorId;

using MediatR;
using MyHostel.Application.Seguridad.Usuario.Dtos;

public class ObtenerUsuarioPorIdQuery(Guid id) : IRequest<UsuarioDto?>
{
    public Guid Id { get; set; } = id;
}
