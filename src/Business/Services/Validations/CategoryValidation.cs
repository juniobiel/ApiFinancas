using Business.Models;
using FluentValidation;

namespace Business.Services.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.CategoryName)
                .MinimumLength(3).MaximumLength(100)
                .WithMessage("O campo precisa ter entre 3 e 100 caracteres");

            RuleFor(c => c.TransactionType)
                .NotNull()
                .WithMessage("Informe o tipo de transação na qual pertence a categoria a ser cadastrada");

            RuleFor(c => c.TransactionType)
                .IsInEnum().NotEqual(TransactionType.Transfer)
                .WithMessage("A categoria não pode ser cadastrada neste tipo de transferencia!");
        }
    }
}
