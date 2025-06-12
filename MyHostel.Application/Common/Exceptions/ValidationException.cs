namespace MyHostel.Application.Common.Exceptions;

public class ValidationException(IDictionary<string, string[]> errors) : Exception("Se encontraron errores de validación.")
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}