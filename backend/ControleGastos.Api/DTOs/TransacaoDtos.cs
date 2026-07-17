using ControleGastos.Api.Models;

namespace ControleGastos.Api.DTOs;

/// <summary>DTO para criação de transação. Id não é enviado pelo cliente.</summary>
public class CriarTransacaoDto
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public Guid PessoaId { get; set; }
}

/// <summary>DTO de resposta para transação criada/listada. Inclui nome da pessoa.</summary>
public class TransacaoResponseDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public Guid PessoaId { get; set; }
    public string PessoaNome { get; set; } = string.Empty;
}