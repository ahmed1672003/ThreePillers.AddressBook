namespace ThreePillers.AddressBook.Application.Middlewares;

public sealed class ExceptionHandler() : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, message) = MapExceptionToResponse(ex);

            var response = new Response
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
            };
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(
                response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );
            await context.Response.WriteAsync(json);
        }
    }

    private static (int statusCode, string message) MapExceptionToResponse(Exception ex) =>
        ex switch
        {
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, ex.Message),
            ValidationException => ((int)HttpStatusCode.BadRequest, ex.Message),
            DomainException => ((int)HttpStatusCode.BadRequest, ex.Message),
            _
                => (
                    (int)HttpStatusCode.InternalServerError,
                    "Oops somthing went wrong!, Please try again later"
                ),
        };
}
