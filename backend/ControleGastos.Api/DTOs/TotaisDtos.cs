namespace ControleGastos.Api.DTOs;

/// DTO de total por pessoa (receitas, despesas, saldo).
public class TotalPorPessoaDto
{
    public Guid PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}

/// DTO de totais gerais consolidados.
public class TotaisGeraisDto
{
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal SaldoLiquido { get; set; }
}

/// DTO de resposta do endpoint GET /api/totais.
public class TotaisResponseDto
{
    public List<TotalPorPessoaDto> Pessoas { get; set; } = new();
    public TotaisGeraisDto TotaisGerais { get; set; } = new();
}