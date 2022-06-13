using Business.Interfaces.Services;
using Business.Models;
using Business.Services.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator )
        {
            _notificator = notificator;
        }

        protected void Notify (string message)
        {
            _notificator.Handle(new Notification(message));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if(validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
