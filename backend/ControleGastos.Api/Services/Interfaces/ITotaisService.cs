using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services.Interfaces;

/// Serviço de consulta de totais.
public interface ITotaisService
{
    /// Calcula totais por pessoa e gerais.
    ///  Saldo = TotalReceitas - TotalDespesas.
    ///  Pessoas sem transações aparecem com zeros.
    Task<TotaisResponseDto> ConsultarAsync();
}