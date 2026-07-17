namespace ControleGastos.Api.Exceptions;

/// Exceção para recurso não encontrado. Mapeada para HTTP 404.
public class NotFoundException : Exception
{
    public NotFoundException(string mensagem)
        : base(mensagem) { }
}