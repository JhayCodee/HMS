using Xunit;
using FluentValidation.TestHelper;
using MyHostel.Application.Seguridad.Usuario.Commands.CrearUsuario;

public class CrearUsuarioCommandValidatorTests
{
    private readonly CrearUsuarioCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CrearUsuarioCommand
        {
            Nombre = "Prueba",
            Email = "invalido",
            Password = "123456"
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
}
