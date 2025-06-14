using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration; // Para acceder a appsettings.json
using Microsoft.Extensions.DependencyInjection; // Para usar IServiceCollection
using MyHostel.Domain.Interfaces; // Interfaces de dominio
using MyHostel.Domain.Interfaces.Seguridad;
using MyHostel.Infrastructure.Persistence; // DbContext
using MyHostel.Infrastructure.Repositories;
using MyHostel.Infrastructure.Repositories.Seguridad; // Implementación de repositorios

namespace MyHostel.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtiene la cadena de conexión desde appsettings.json (clave: "DefaultConnection")
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Registra HostelDbContext en el contenedor de dependencias usando PostgreSQL
        services.AddDbContext<HostelDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Registra el repositorio de habitaciones
        // Siempre que se pida un IHabitacionRepository, se inyectará HabitacionRepository
        services.AddScoped<IHabitacionRepository, HabitacionRepository>();


        #region SEGURIDAD

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        #endregion

        // Devuelve el contenedor con las nuevas configuraciones
        return services;
    }
}
