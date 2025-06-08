using MyHostel.Infrastructure.Extensions;
using MyHostel.Application.Extensions;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar los servicios de infraestructura (DbContext, etc.)
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

// Configuración del middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
