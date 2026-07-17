namespace ControleGastos.Api.Models;

/// Representa uma transação financeira vinculada a uma pessoa.
public class Transacao
{
    /// UUID gerado automaticamente.
    public Guid Id { get; set; }

    /// Descrição da transação. Obrigatória, máximo 200 caracteres, não pode ser vazio.
    public string Descricao { get; set; } = string.Empty;

    /// Valor monetário sempre positivo. O tipo define se é entrada ou saída (Receita ou Despesa).
    public decimal Valor { get; set; }

    /// Tipo da transação: Despesa (0) ou Receita (1).
    public TipoTransacao Tipo { get; set; }

    /// FK para a pessoa vinculada.
    public Guid PessoaId { get; set; }

    /// Pessoa vinculada à transação.
    public Pessoa Pessoa { get; set; } = null!;
}