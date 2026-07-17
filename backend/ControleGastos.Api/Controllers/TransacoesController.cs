using Microsoft.AspNetCore.Mvc;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Controllers;

/// Controller para gerenciamento de transações financeiras.
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _service;

    public TransacoesController(ITransacaoService service)
    {
        _service = service;
    }

    
    /// Lista todas as transações, ordenadas por descrição, incluindo nome da pessoa.
    [HttpGet]
    [ProducesResponseType(typeof(List<TransacaoResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TransacaoResponseDto>>> Listar()
    {
        var transacoes = await _service.ListarAsync();
        return Ok(transacoes);
    }

    
    /// Cria uma nova transação.
    ///  Pessoa deve existir.
    ///  Menor de 18 anos só registra despesas.
    ///  Valor deve ser maior que zero.
    [HttpPost]
    [ProducesResponseType(typeof(TransacaoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransacaoResponseDto>> Criar([FromBody] CriarTransacaoDto dto)
    {
        var transacao = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(Listar), new { id = transacao.Id }, transacao);
    }
}