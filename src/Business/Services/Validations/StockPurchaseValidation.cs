using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class StockPurchaseValidation : AbstractValidator<StockPurchase>
    {
        public StockPurchaseValidation()
        {
            RuleFor(s => s.StockPrice)
                .GreaterThan(0)
                .WithMessage("O preço precisa ser maior que 0");

            RuleFor(s => s.StockQtd)
                .GreaterThan(0)
                .WithMessage("A quantidade precisa ser maior que 0");

            RuleFor(s => s.StockTicker)
                .NotEmpty().NotNull()
                .MaximumLength(7)
                .MinimumLength(3)
                .WithMessage("Descreva o Ticker com até 7 caracteres");

            RuleFor(s => s.PurchaseDate)
                .NotEmpty().NotNull()
                .WithMessage("Defina a data da compra");
        }
    }
}
