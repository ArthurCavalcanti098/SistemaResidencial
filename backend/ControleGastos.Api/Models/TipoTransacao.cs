namespace ControleGastos.Api.Models;

/// <summary>
/// Classifica uma transação financeira como entrada (receita) ou saída (despesa).
/// </summary>
public enum TipoTransacao
{
    /// <summary>Saída de valor</summary>
    Despesa = 0,
    /// <summary>Entrada de valor</summary>
    Receita = 1
}