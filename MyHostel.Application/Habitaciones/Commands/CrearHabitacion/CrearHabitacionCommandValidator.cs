using FluentValidation;

namespace MyHostel.Application.Habitaciones.Commands.CrearHabitacion;

public class CrearHabitacionCommandValidator : AbstractValidator<CrearHabitacionCommand>
{
    public CrearHabitacionCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Capacidad).InclusiveBetween(1, 10);
        RuleFor(x => x.PrecioPorNoche).GreaterThan(0);
    }
}
