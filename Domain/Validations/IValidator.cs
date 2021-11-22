using System;

namespace PaymentGateway.Domain.Validations
{
    public interface IValidator
    {
        void AssertNotNull(object value, string property);
        void AssertNotNull(object value, string property, out bool isNull);
        void AssertTrue(bool value, string property, string message);
        void AssertEquality(string value1, string value2, string property, string message);
        void AssertRegex(string regex, string value, string property);
        void AssertGuid(Guid value, string property, string message);
        void AssertGreaterOrEqualThan(decimal value1, decimal value2, string property, string message);
        void AssertPositive(decimal value, string property, string message);
        void AssertCustom(bool assertionResult, string property, string message);
        bool IsDomainValid();
    }
}