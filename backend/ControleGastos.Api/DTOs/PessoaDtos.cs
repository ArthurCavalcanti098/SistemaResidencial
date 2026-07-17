namespace ControleGastos.Api.DTOs;

/// <summary>DTO para criação de pessoa. Id não é enviado pelo cliente.</summary>
public class CriarPessoaDto
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}

/// <summary>DTO de resposta para pessoa criada/listada.</summary>
public class PessoaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}