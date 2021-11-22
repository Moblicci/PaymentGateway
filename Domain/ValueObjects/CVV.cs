using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects
{
    public class CVV : IValueObject<CVV>
    {
        private readonly string _value;
        private const string RegExForValidation = @"^[0-9]{3,4}$";
        
        public CVV(string value)
        {
            _value = value;
        }

        public override string ToString() => _value;

        public void Validate(IValidator validator)
        {
            validator.AssertNotNull(_value, "CVV");
            validator.AssertRegex(RegExForValidation, _value, "CVV");
        }
    }
}
