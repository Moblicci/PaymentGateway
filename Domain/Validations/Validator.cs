using System;
using System.Text.RegularExpressions;
using PaymentGateway.Domain.Notifications;

namespace PaymentGateway.Domain.Validations
{
    //TODO #4
    //TODO #5
    public class Validator : IValidator
    {
        private readonly INotificationHandler _domainNotification;

        public Validator(INotificationHandler domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public void AssertNotNull(object value, string property)
        {
            bool isNull = false;
            this.AssertNotNull(value, property, out isNull);
        }


        public void AssertNotNull(object value, string property, out bool isNull)
        {
            if (value is null)
            {
                _domainNotification.Handle(new DomainNotification($"{property}", "Cannot be bull"));
                isNull = true;
            }
            isNull = false;
        }

        public void AssertRegex(string regex, string value, string property)
        {
            Regex regexToValidate = new Regex(regex);
            Match match = regexToValidate.Match(value);

            if(!match.Success)
                _domainNotification.Handle(new DomainNotification($"{property}", "Invalid"));
        }

        public void AssertGuid(Guid value, string property, string message)
        {
            if(value.Equals(Guid.Empty))
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }

        public void AssertGreaterOrEqualThan(decimal value1, decimal value2, string property, string message)
        {
            if(value1 < value2)
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }

        public void AssertEquality(string value1, string value2, string property, string message)
        {
            if(value1 != value2)
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }

        public bool IsDomainValid() => !_domainNotification.HasNotifications();

        public void AssertPositive(decimal value, string property, string message)
        {
            if(value <= 0)
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }

        public void AssertTrue(bool value, string property, string message)
        {
            if(!value)
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }

        public void AssertCustom(bool assertionResult, string property, string message)
        {
            if(assertionResult)
                _domainNotification.Handle(new DomainNotification($"{property}", $"{message}"));
        }
    }
}