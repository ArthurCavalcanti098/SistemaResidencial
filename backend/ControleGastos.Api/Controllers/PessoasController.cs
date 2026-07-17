using Microsoft.AspNetCore.Mvc;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de pessoas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _service;

    public PessoasController(IPessoaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas, ordenadas por nome.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<PessoaResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PessoaResponseDto>>> Listar()
    {
        var pessoas = await _service.ListarAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Cria uma nova pessoa. O Id é gerado automaticamente pelo servidor .
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PessoaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PessoaResponseDto>> Criar([FromBody] CriarPessoaDto dto)
    {
        var pessoa = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(Listar), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Exclui uma pessoa e todas as suas transações ( via ON DELETE CASCADE).
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Excluir(Guid id)
    {
        await _service.ExcluirAsync(id);
        return NoContent();
    }
}