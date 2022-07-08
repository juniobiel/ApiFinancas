using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class StockValidation : AbstractValidator<Stock>
    {
        public StockValidation()
        {
            RuleFor(s => s.StockTicker)
                .NotEmpty().NotNull()
                .MinimumLength(3)
                .MaximumLength(7)
                .WithMessage("Descreva o Ticker com até 7 caracteres");

            RuleFor(s => s.StockQt)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Insira uma quantidade maior que 0");


        }
    }
}
