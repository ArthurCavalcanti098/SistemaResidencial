namespace ControleGastos.Api.DTOs;

/// <summary>DTO de total por pessoa (receitas, despesas, saldo).</summary>
public class TotalPorPessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}

/// <summary>DTO de totais gerais consolidados.</summary>
public class TotaisGeraisDto
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoLiquido { get; set; }
}

/// <summary>DTO de resposta do endpoint GET /api/totais.</summary>
public class TotaisResponseDto
{
    public List<TotalPorPessoaDto> Pessoas { get; set; } = new();
    public TotaisGeraisDto TotaisGerais { get; set; } = new();
}