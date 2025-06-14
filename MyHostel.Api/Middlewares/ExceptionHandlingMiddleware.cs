using MyHostel.Application.Common.Exceptions;
using MyHostel.Api.Responses;

namespace MyHostel.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> log)
{
    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (NotFoundException nf)
        {
            log.LogWarning(nf, "NotFoundException capturada: {ErrorMessage}", nf.Message);
            ctx.Response.StatusCode = StatusCodes.Status404NotFound;
            await ctx.Response.WriteAsJsonAsync(new ApiErrorResponse(
                StatusCodes.Status404NotFound,
                "Recurso no encontrado",
                nf.Message));
        }
        catch (FluentValidation.ValidationException ve)
        {
            log.LogWarning(ve, "ValidationException capturada: {ErrorMessage}", ve.Message);

            var errors = ve.Errors?
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            ctx.Response.StatusCode = 400;
            await ctx.Response.WriteAsJsonAsync(new ApiErrorResponse(
                400,
                "Error de validación",
                "Uno o más errores de validación ocurrieron.",
                errors
            ));
        }
        catch (BadRequestException br)
        {
            log.LogWarning(br, "BadRequestException capturada: {ErrorMessage}", br.Message);
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            await ctx.Response.WriteAsJsonAsync(new ApiErrorResponse(
                StatusCodes.Status400BadRequest,
                "Solicitud incorrecta",
                br.Message));
        }
        catch (UnauthorizedAccessException ua)
        {
            log.LogWarning(ua, "UnauthorizedException capturada: {ErrorMessage}", ua.Message);
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await ctx.Response.WriteAsJsonAsync(new ApiErrorResponse(
                StatusCodes.Status401Unauthorized,
                "No autorizado",
                ua.Message
            ));
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error inesperado");
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new ApiErrorResponse(
                StatusCodes.Status500InternalServerError,
                "Error interno del servidor",
                "Ocurrió un error inesperado. Contacte al administrador."));
        }
    }
}
