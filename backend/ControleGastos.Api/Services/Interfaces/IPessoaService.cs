using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// Serviço de gerenciamento de pessoas.
public interface IPessoaService
{
    /// Lista todas as pessoas ordenadas por nome.
    Task<List<PessoaResponseDto>> ListarAsync();

    /// Cria uma nova pessoa com Id gerado automaticamente.
    Task<PessoaResponseDto> CriarAsync(CriarPessoaDto dto);

    /// Exclui pessoa e todas as transações vinculadas (via ON DELETE CASCADE no banco de dados).
    Task ExcluirAsync(Guid id);
}