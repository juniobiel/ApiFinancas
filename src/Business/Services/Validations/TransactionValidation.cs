using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class TransactionValidation : AbstractValidator<Transaction>
    {
        public TransactionValidation()
        {
            RuleFor(t => t.TransactionType)
                .NotNull()
                .WithMessage("É preciso informar o tipo da transação");

            RuleFor(t => t.Value)
                .GreaterThan(0)
                .WithMessage("O valor da transação precisa ser maior que 0");

            RuleFor(t => t.TransactionDate)
                .NotNull()
                .WithMessage("Informe uma data para a transação");

            RuleFor(t => t.Description)
                .MinimumLength(2)
                .MaximumLength(100)
                .WithMessage("A descrição precisa ter entre 2 e 100 caracteres");
        }
    }
}
