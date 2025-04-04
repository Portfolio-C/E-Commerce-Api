using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Api.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }
    private static async Task HandleAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, message) = GetErrorCode(exception);
        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = message
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static (int statusCode, string title, string message) GetErrorCode(Exception ex)
        => ex switch
        {
            EntityNotFoundException => ((int)HttpStatusCode.NotFound, "NotFound", ex.Message),
            _ => ((int)HttpStatusCode.InternalServerError, "Internal server error", "Unexpected error occurred. Please, try again later.")
        };

}
