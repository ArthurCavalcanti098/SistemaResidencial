namespace ControleGastos.Api.Models;

/// <summary>
/// Representa uma transação financeira vinculada a uma pessoa.
/// </summary>
public class Transacao
{
    /// <summary>Identificador único gerado automaticamente (UUID).</summary>
    public Guid Id { get; set; }

    /// <summary>Descrição da transação. Obrigatória, máximo 200 caracteres.</summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>Valor monetário sempre positivo. O tipo define se é entrada ou saída.</summary>
    public decimal Valor { get; set; }

    /// <summary>Tipo da transação: Despesa (0) ou Receita (1).</summary>
    public TipoTransacao Tipo { get; set; }

    /// <summary>FK para a pessoa vinculada.</summary>
    public Guid PessoaId { get; set; }

    /// <summary>Pessoa vinculada à transação.</summary>
    public Pessoa Pessoa { get; set; } = null!;
}