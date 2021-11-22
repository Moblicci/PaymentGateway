using System;
using System.Collections.Generic;

namespace PaymentGateway.Domain.Notifications
{
    public interface INotificationHandler : IDisposable
    {

        void Handle(DomainNotification args);
        IEnumerable<DomainNotification> Notify();
        bool HasNotifications();
    }
}