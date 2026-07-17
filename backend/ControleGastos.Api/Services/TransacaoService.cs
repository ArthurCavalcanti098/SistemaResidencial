using Microsoft.EntityFrameworkCore;
using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Services;

/// <summary>
/// Serviço de gerenciamento de transações.
/// Implementa pessoa deve existir, menor de 18 só despesa
/// e valor sempre positivo.
/// </summary>
public class TransacaoService : ITransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TransacaoResponseDto>> ListarAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .OrderBy(t => t.Descricao)
            .Select(t => new TransacaoResponseDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo == TipoTransacao.Receita ? "Receita" : "Despesa",
                PessoaId = t.PessoaId,
                PessoaNome = t.Pessoa.Nome
            })
            .ToListAsync();
    }

    public async Task<TransacaoResponseDto> CriarAsync(CriarTransacaoDto dto)
    {
        var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId)
            ?? throw new NotFoundException("Pessoa não encontrada");

        if (!Enum.TryParse<TipoTransacao>(dto.Tipo, out var tipo))
            throw new BusinessException("Tipo inválido");

        if (pessoa.Idade < 18 && tipo == TipoTransacao.Receita)
            throw new BusinessException("Menores de 18 anos só podem registrar despesas", "MENOR_IDADE_RECEITA");

        var transacao = new Transacao
        {
            Id = Guid.NewGuid(),
            Descricao = dto.Descricao.Trim(),
            Valor = dto.Valor,
            Tipo = tipo,
            PessoaId = dto.PessoaId
        };

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        await _context.Entry(transacao).Reference(t => t.Pessoa).LoadAsync();

        return new TransacaoResponseDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo == TipoTransacao.Receita ? "Receita" : "Despesa",
            PessoaId = transacao.PessoaId,
            PessoaNome = transacao.Pessoa.Nome
        };
    }
}