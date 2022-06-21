using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class AccountValidation : AbstractValidator<Account>
    {
        public AccountValidation()
        {
            RuleFor(a => a.AccountName)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre " +
                "{MinLength} e {MaxLength} caracteres");

            RuleFor(a => a.AccountBalance)
                .ScalePrecision(2, 20);
        }
    }
}
