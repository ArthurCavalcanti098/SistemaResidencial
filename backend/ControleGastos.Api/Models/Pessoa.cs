namespace ControleGastos.Api.Models;

/// <summary>
/// Representa uma pessoa cadastrada na residência.
/// </summary>
public class Pessoa
{
    /// <summary>Identificador único gerado automaticamente (UUID).</summary>
    public Guid Id { get; set; }

    /// <summary>Nome da pessoa. Obrigatório, máximo 100 caracteres.</summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>Idade em anos completos. Deve estar entre 0 e 150.</summary>
    public int Idade { get; set; }

    /// <summary>Transações vinculadas a esta pessoa.</summary>
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}