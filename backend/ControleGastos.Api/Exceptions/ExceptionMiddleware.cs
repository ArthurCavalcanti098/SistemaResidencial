using System.Text.Json;

namespace ControleGastos.Api.Exceptions;

/// Middleware que intercepta exceções de domínio e as mapeia para respostas HTTP padronizadas.
/// BusinessException → 400, NotFoundException → 404, demais → 500.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json; charset=utf-8";
            var body = JsonSerializer.Serialize(new { mensagem = ex.Message });
            await context.Response.WriteAsync(body);
        }
        catch (BusinessException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json; charset=utf-8";
            var body = JsonSerializer.Serialize(new { mensagem = ex.Message, codigo = ex.Codigo });
            await context.Response.WriteAsync(body);
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json; charset=utf-8";
            var body = JsonSerializer.Serialize(new { mensagem = "Erro interno do servidor" });
            await context.Response.WriteAsync(body);
        }
    }
}