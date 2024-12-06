namespace api.GlobalException;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // Call the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled exception: {ex.Message}", ex); // Log the exception

            // Return a structured error response
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var errorResponse = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Details = ex.Message // You can also customize the details for internal use or logging
            };
            await httpContext.Response.WriteAsJsonAsync(errorResponse); // Send the error response
        }
    }
}
