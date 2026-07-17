using Microsoft.EntityFrameworkCore;
using ControleGastos.Api.Data;
using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Services.Interfaces;

namespace ControleGastos.Api.Services;

/// <summary>
/// Serviço de gerenciamento de pessoas.
/// Implementa criação com Id automático, listagem ordenada por Nome
/// e exclusão com cascade delete.
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PessoaResponseDto>> ListarAsync()
    {
        return await _context.Pessoas
            .OrderBy(p => p.Nome)
            .Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade
            })
            .ToListAsync();
    }

    public async Task<PessoaResponseDto> CriarAsync(CriarPessoaDto dto)
    {
        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = dto.Nome.Trim(),
            Idade = dto.Idade
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        return new PessoaResponseDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }

    public async Task ExcluirAsync(Guid id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id)
            ?? throw new NotFoundException("Pessoa não encontrada");

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
    }
}