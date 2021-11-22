using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects
{
    public class CardNumber : IValueObject<CardNumber>
    {
        private readonly string _value;
        
        public CardNumber(string value)
        {
            _value = value;
        }

        public string CardHint => $"{_value.Substring(0, 6)}XXXXXX{_value.Substring(12, _value.Length - 12)}";

        public override string ToString() => _value;

        public void Validate(IValidator validator)
        {
            validator.AssertNotNull(_value, "CardNumber");

            var company = GetCardCompany(_value);
            string regexForValidation = string.Empty;

            if(company == "Visa")
                regexForValidation = @"^4[0-9]{12}(?:[0-9]{3})?$";//Visa Card Regex: just for testing purpose

            validator.AssertRegex(regexForValidation, _value, "CardNumber");
        }

        private string GetCardCompany(string value)
        {
            switch (value.Substring(0, 1))
            {
                case("3"):
                    return "American Express";
                case("4"):
                    return "Visa";
                case("5"):
                    return "MasterCard";
                case("6"):
                    return "Discover";
                default:
                    return null;
            }
        }
    }
}
