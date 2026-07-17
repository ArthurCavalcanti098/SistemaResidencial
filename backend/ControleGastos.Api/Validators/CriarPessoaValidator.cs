using FluentValidation;
using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Validators;

/// <summary>
/// Validação estrutural do DTO de criação de pessoa.
/// </summary>
public class CriarPessoaValidator : AbstractValidator<CriarPessoaDto>
{
    public CriarPessoaValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Idade)
            .NotEmpty().WithMessage("Idade é obrigatória")
            .InclusiveBetween(0, 150).WithMessage("Idade deve estar entre 0 e 150");
    }
}