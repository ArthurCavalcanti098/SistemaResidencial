using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// <summary>
/// Serviço de gerenciamento de transações.
/// </summary>
public interface ITransacaoService
{
    /// <summary>Lista todas as transações ordenadas por descrição, incluindo nome da pessoa.</summary>
    Task<List<TransacaoResponseDto>> ListarAsync();

    /// <summary>
    /// Cria uma nova transação.
    ///  Pessoa deve existir.
    ///  Menor de 18 anos só pode registrar despesas.
    ///  Valor sempre positivo.
    /// </summary>
    Task<TransacaoResponseDto> CriarAsync(CriarTransacaoDto dto);
}