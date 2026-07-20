using ControleGastos.Api.Exceptions;
using System.Net;
using System.Text.Json;

namespace ControleGastos.Api.Middlewares;

/// <summary>
/// Middleware global para interceptar e tratar exceções da aplicação.
/// Centraliza o tratamento de erros, formatando a resposta HTTP de acordo com o tipo de exceção.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Uma exceção não tratada ocorreu durante o processamento da requisição.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new { message = exception.Message };

        switch (exception)
        {
            case BusinessRuleException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new { message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde." };
                break;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
