using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// <summary>
/// Serviço de gerenciamento de pessoas.
/// </summary>
public interface IPessoaService
{
    /// <summary>Lista todas as pessoas ordenadas por nome.</summary>
    Task<List<PessoaResponseDto>> ListarAsync();

    /// <summary>Cria uma nova pessoa com Id gerado automaticamente.</summary>
    Task<PessoaResponseDto> CriarAsync(CriarPessoaDto dto);

    /// <summary>
    /// Exclui pessoa e todas as transações vinculadas (via ON DELETE CASCADE no banco de dados).
    /// </summary>
    Task ExcluirAsync(Guid id);
}