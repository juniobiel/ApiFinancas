using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class StockTransactionValidation : AbstractValidator<StockTransaction>
    {
        public StockTransactionValidation()
        {
            RuleFor(s => s.StockPrice)
                .GreaterThan(0)
                .WithMessage("O preço precisa ser maior que 0");

            RuleFor(s => s.StockQt)
                .GreaterThan(0)
                .WithMessage("A quantidade precisa ser maior que 0");

            RuleFor(s => s.StockTicker)
                .NotEmpty().NotNull()
                .MaximumLength(7)
                .MinimumLength(3)
                .WithMessage("Descreva o Ticker com até 7 caracteres");

            RuleFor(s => s.TransactionDate)
                .NotEmpty().NotNull()
                .WithMessage("Defina a data da compra");
        }
    }
}
