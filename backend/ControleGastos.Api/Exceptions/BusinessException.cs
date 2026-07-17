namespace ControleGastos.Api.Exceptions;

/// Exceção para violações de regra de negócio. Mapeada para HTTP 400.
public class BusinessException : Exception
{
    public string Codigo { get; }

    public BusinessException(string mensagem, string codigo = "REGRA_NEGOCIO")
        : base(mensagem)
    {
        Codigo = codigo;
    }
}