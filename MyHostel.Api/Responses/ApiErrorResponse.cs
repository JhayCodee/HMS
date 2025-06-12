namespace MyHostel.Api.Responses;

public class ApiErrorResponse(int status, string title, string message, object? errors = null)
{
    public int Status { get; set; } = status;
    public string Title { get; set; } = title;
    public string Message { get; set; } = message;
    public object? Errors { get; set; } = errors;
}
