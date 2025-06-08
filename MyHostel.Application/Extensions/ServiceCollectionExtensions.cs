using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyHostel.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper para mapeos DTO / entidad
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // MediatR  
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
        );

        // FluentValidation desde el ensamblado actual
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
