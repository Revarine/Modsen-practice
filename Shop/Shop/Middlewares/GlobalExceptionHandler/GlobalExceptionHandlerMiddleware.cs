using System.Net;
using System.Text.Json;

namespace Shop.Middlewares.GlobalExceptionHandler
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    Message = "An error occurred.",
                    ErrorCode = "InternalServerError"
                };

                var result = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
