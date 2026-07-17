namespace ControleGastos.Api.Exceptions;

/// <summary>
/// Exceção para recurso não encontrado. Mapeada para HTTP 404.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string mensagem)
        : base(mensagem) { }
}