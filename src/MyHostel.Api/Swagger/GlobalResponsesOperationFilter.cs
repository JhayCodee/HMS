using Microsoft.OpenApi.Models;
using MyHostel.Api.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyHostel.Api.Swagger;

public class GlobalResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var httpMethod = context.ApiDescription.HttpMethod;

        // Positivos
        if (httpMethod == HttpMethods.Get)
        {
            operation.Responses.TryAdd("200", CreateResponse("OK", typeof(ApiResponse<>), context));
            operation.Responses.TryAdd("404", CreateErrorResponse(404, "Not Found"));
        }
        else if (httpMethod == HttpMethods.Post)
        {
            operation.Responses.TryAdd("201", CreateResponse("Created", typeof(ApiResponse<>), context));
            operation.Responses.TryAdd("400", CreateErrorResponse(400, "Bad Request"));
        }
        else if (httpMethod == HttpMethods.Put || httpMethod == HttpMethods.Patch)
        {
            operation.Responses.TryAdd("200", CreateResponse("OK", typeof(ApiResponse<>), context));
            operation.Responses.TryAdd("400", CreateErrorResponse(400, "Bad Request"));
            operation.Responses.TryAdd("404", CreateErrorResponse(404, "Not Found"));
        }
        else if (httpMethod == HttpMethods.Delete)
        {
            operation.Responses.TryAdd("204", new OpenApiResponse { Description = "No Content" });
            operation.Responses.TryAdd("404", CreateErrorResponse(404, "Not Found"));
        }

        // Siempre adicionar error general
        operation.Responses.TryAdd("500", CreateErrorResponse(500, "Internal Server Error"));
    }

    private OpenApiResponse CreateErrorResponse(int status, string title) =>
        new OpenApiResponse
        {
            Description = title,
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.Schema,
                            Id = nameof(ApiErrorResponse)
                        }
                    }
                }
            }
        };

    private OpenApiResponse CreateResponse(string title, Type genericType, OperationFilterContext context)
    {
        var responseType = context.MethodInfo
            .ReturnType
            .GetGenericArguments()
            .FirstOrDefault() ?? typeof(object);

        var schema = new OpenApiSchema
        {
            Type = "object",
            Properties = new Dictionary<string, OpenApiSchema>
            {
                ["data"] = new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.Schema,
                        Id = responseType.Name
                    }
                }
            }
        };

        return new OpenApiResponse
        {
            Description = title,
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType { Schema = schema }
            }
        };
    }
}
