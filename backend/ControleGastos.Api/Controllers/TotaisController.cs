using Microsoft.AspNetCore.Mvc;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Controllers;

/// <summary>
/// Controller para consulta de totais consolidados.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TotaisController : ControllerBase
{
    private readonly ITotaisService _service;

    public TotaisController(ITotaisService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna totais por pessoa e gerais.
    ///  Saldo = TotalReceitas - TotalDespesas.
    ///  Pessoas sem transações aparecem com zeros.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(TotaisResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TotaisResponseDto>> Consultar()
    {
        var totais = await _service.ConsultarAsync();
        return Ok(totais);
    }
}