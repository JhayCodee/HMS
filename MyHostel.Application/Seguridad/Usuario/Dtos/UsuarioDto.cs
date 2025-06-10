namespace MyHostel.Application.Seguridad.Usuario.Dtos;

// Define qué datos vamos a enviar/responder al interactuar con usuarios
public class UsuarioDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool Activo { get; set; }
}
