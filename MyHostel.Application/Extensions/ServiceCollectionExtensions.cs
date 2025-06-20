﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyHostel.Application.Common.Validations;
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
        services
        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Registro del comportamiento de validación
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



        return services;
    }
}
