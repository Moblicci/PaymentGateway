using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects {
    public class Currency : IValueObject<Currency>
    {
        public string CurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }

        public Currency(string currencyCode, int decimalPlace)
        {
            CurrencyCode = currencyCode;
            DecimalPlaces = decimalPlace;
        }

        public Currency()
        {
            
        }

        public static Currency None = new Currency();

        public void Validate(IValidator validator)
        {
            validator.AssertNotNull(CurrencyCode, "CurrencyCode");
        }
    }
}