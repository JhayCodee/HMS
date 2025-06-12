using MyHostel.Infrastructure.Extensions;
using MyHostel.Application.Extensions;
using MyHostel.Api.Middlewares;
using Serilog;
using Microsoft.OpenApi.Models;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Information("Inicializando MyHostel API...");

var builder = WebApplication.CreateBuilder(args);

// Usar Serilog como logger
builder.Host.UseSerilog();

// Agregar los servicios de infraestructura y aplicación
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Agregar controladores
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyHostel API", Version = "v1" });
});

var app = builder.Build();

// Middleware para manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configuración de entorno
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
