using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyHostel.Api.Middlewares;
using MyHostel.Api.Responses;
using MyHostel.Api.Swagger;
using MyHostel.Application.Extensions;
using MyHostel.Infrastructure.Extensions;
using Serilog;
using System.Text;
using System.Text.Json;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console() // Logs en consola
    .WriteTo.File("Logs/log-.txt", // Logs en archivos diarios
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

Log.Information("Inicializando MyHostel API...");

var builder = WebApplication.CreateBuilder(args);

// Reemplaza el logger por defecto por Serilog
builder.Host.UseSerilog();

// Registra las capas de infraestructura y aplicación
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Agrega soporte para controladores de API
builder.Services.AddControllers();

// Configuración de Swagger para la documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyHostel API", Version = "v1" });

    // Respuestas globales estandarizadas
    c.OperationFilter<GlobalResponsesOperationFilter>();

    // Seguridad para autenticación JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración de autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found in configuration.");
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        // Personaliza respuesta para errores 401
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse(); // Evita que la respuesta predeterminada se envíe
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new ApiErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    "No autorizado",
                    "El token es inválido o no fue proporcionado.");

                var json = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }
        };
    });

// Autorización (roles, claims, políticas si se agregan)
builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware global para manejar excepciones controladas y formatearlas
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Activa Swagger solo en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS y seguridad
app.UseHttpsRedirection();

// Autenticación antes de autorización
app.UseAuthentication();
app.UseAuthorization();

// Manejo estandarizado para respuestas 404, 400 (excluyendo 401 que se maneja en JWT Events)
app.UseStatusCodePages(async ctx =>
{
    var response = ctx.HttpContext.Response;
    var code = response.StatusCode;

    if (code == StatusCodes.Status401Unauthorized) return; // Ya lo maneja JWT

    var message = code switch
    {
        404 => "Recurso no encontrado.",
        400 => "Solicitud incorrecta.",
        _ => "Ocurrió un error."
    };

    response.ContentType = "application/json";
    var error = new ApiErrorResponse(code, message, message);
    await response.WriteAsJsonAsync(error);
});

// Mapeo de rutas de controladores
app.MapControllers();

// Arranque de la aplicación
app.Run();
