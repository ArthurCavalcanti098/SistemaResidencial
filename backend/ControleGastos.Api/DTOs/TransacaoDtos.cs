using ControleGastos.Api.Models;

namespace ControleGastos.Api.DTOs;

/// DTO para criação de transação. O Id não é enviado pelo cliente.
public class CriarTransacaoDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public Guid PessoaId { get; set; }
}

/// DTO de resposta para transação criada/listada. Incluindo o nome da pessoa.
public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public Guid PessoaId { get; set; }
    public string PessoaNome { get; set; } = string.Empty;
}