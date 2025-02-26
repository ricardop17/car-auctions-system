using System.Net;
using CarAuctionsSystem.Application.Models;

namespace CarAuctionsSystem.Api.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Message : {message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = new ResultDto<string?>
        {
            StatusCode = context.Response.StatusCode,
            Error = exception.Message,
        }.ToJson();

        await context.Response.WriteAsync(result);
    }
}
