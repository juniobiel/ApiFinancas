using Business.Services.Notifications;

namespace Business.Interfaces.Services
{
    public interface INotificator
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle( Notification notification );
    }
}
