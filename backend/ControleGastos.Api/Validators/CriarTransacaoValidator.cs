using FluentValidation;
using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Validators;

/// Validação estrutural do DTO de criação de transação.
public class CriarTransacaoValidator : AbstractValidator<CriarTransacaoDto>
{
    public CriarTransacaoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MaximumLength(200).WithMessage("Descrição deve ter no máximo 200 caracteres");

        RuleFor(x => x.Valor)
            .NotEmpty().WithMessage("Valor é obrigatório")
            .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("Tipo é obrigatório")
            .Must(t => t == "Despesa" || t == "Receita").WithMessage("Tipo inválido");

        RuleFor(x => x.PessoaId)
            .NotEmpty().WithMessage("Pessoa é obrigatória");
    }
}