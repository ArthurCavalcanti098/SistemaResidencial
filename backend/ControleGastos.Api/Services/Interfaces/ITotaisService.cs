using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// <summary>
/// Serviço de consulta de totais.
/// </summary>
public interface ITotaisService
{
    /// <summary>
    /// Calcula totais por pessoa e gerais.
    ///  Saldo = TotalReceitas - TotalDespesas.
    ///  Pessoas sem transações aparecem com zeros.
    /// </summary>
    Task<TotaisResponseDto> ConsultarAsync();
}