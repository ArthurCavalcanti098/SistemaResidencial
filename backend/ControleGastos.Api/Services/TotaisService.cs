using Microsoft.EntityFrameworkCore;
using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Services;

/// <summary>
/// Serviço de consulta de totais.
///  Saldo = TotalReceitas - TotalDespesas para cada pessoa.
///  Pessoas sem transações aparecem com zeros.
/// Totais gerais são a soma dos totais de todas as pessoas.
/// </summary>
public class TotaisService : ITotaisService
{
    private readonly AppDbContext _context;

    public TotaisService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TotaisResponseDto> ConsultarAsync()
    {
        var pessoas = await _context.Pessoas
            .OrderBy(p => p.Nome)
            .ToListAsync();

        var pessoasIds = pessoas.Select(p => p.Id).ToList();

        var totaisRaw = await _context.Transacoes
            .Where(t => pessoasIds.Contains(t.PessoaId))
            .GroupBy(t => t.PessoaId)
            .Select(g => new
            {
                PessoaId = g.Key,
                TotalReceitas = g.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
                TotalDespesas = g.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor)
            })
            .ToListAsync();

        var lookup = totaisRaw.ToDictionary(t => t.PessoaId);

        var totaisPorPessoa = pessoas.Select(p => new TotalPorPessoaDto
        {
            PessoaId = p.Id,
            Nome = p.Nome,
            TotalReceitas = lookup.TryGetValue(p.Id, out var t) ? t.TotalReceitas : 0,
            TotalDespesas = lookup.TryGetValue(p.Id, out var d) ? d.TotalDespesas : 0,
            Saldo = (lookup.TryGetValue(p.Id, out var s) ? s.TotalReceitas - s.TotalDespesas : 0)
        }).ToList();

        return new TotaisResponseDto
        {
            Pessoas = totaisPorPessoa,
            TotaisGerais = new TotaisGeraisDto
            {
                TotalReceitas = totaisPorPessoa.Sum(p => p.TotalReceitas),
                TotalDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas),
                SaldoLiquido = totaisPorPessoa.Sum(p => p.Saldo)
            }
        };
    }
}