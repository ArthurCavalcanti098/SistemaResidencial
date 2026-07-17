namespace ControleGastos.Api.Models;

/// Representa uma pessoa cadastrada na residência.
public class Pessoa
{
    /// UUID gerado automaticamente.
    public Guid Id { get; set; }

    /// Nome da pessoa. Obrigatório, máximo 100 caracteres, não pode ser vazio.
    public string Nome { get; set; } = string.Empty;

    /// Idade em anos completos. Deve estar entre 0 e 150 (Não pode ser negativo e ninguem vai ser tão velho assim).
    public int Idade { get; set; }

    /// Transações vinculadas a esta pessoa.
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}