namespace PaymentGateway.Domain.Validations
{
    public interface IValidatable
    {
        void Validate(IValidator validator);
    }
}