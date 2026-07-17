namespace ControleGastos.Api.Models;

/// Classifica uma transação financeira como entrada (receita) ou saída (despesa).
public enum TipoTransacao
{
    /// Saída de valor (despesa)
    Despesa = 0,
    /// Entrada de valor (receita)
    Receita = 1
}