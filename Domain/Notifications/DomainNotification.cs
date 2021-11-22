using System;

namespace PaymentGateway.Domain.Notifications
{
    public class DomainNotification
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            Date = DateTime.Now;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime Date { get; }
    }
}