using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects {
    public interface IValueObject<T> : IValidatable where T : class
    {

    }
}