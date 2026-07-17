namespace ControleGastos.Api.DTOs;

/// DTO para criação de pessoa. Id não é enviado pelo cliente.
public class CriarPessoaDto
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}

/// DTO de resposta para pessoa criada/listada.
public class PessoaResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}