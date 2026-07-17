using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// Serviço de gerenciamento de transações.
public interface ITransacaoService
{
    /// Lista todas as transações ordenadas por descrição, incluindo nome da pessoa.
    Task<List<TransacaoResponseDto>> ListarAsync();

    /// Cria uma nova transação.
    /// Pessoa deve existir.
    /// Menor de 18 anos só pode registrar despesas.
    /// Valor sempre positivo.
    Task<TransacaoResponseDto> CriarAsync(CriarTransacaoDto dto);
}