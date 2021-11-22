using System.Collections.Generic;

namespace PaymentGateway.Domain.Notifications
{
    public class NotificationHandler : INotificationHandler
    {
        private List<DomainNotification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Handle(DomainNotification args)
        {
            _notifications.Add(args);
        }

        public IEnumerable<DomainNotification> Notify()
        {
            
            return GetValue();
        }

        public bool HasNotifications()
        {
            return GetValue().Count > 0;
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        private List<DomainNotification> GetValue()
        {
            return _notifications;
        }
    }
}