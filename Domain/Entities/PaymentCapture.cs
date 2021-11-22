namespace PaymentGateway.Domain.Entities
{
    using System;
    using Domain.ValueObjects;
    using PaymentGateway.Domain.Validations;

    public class PaymentCapture : IEntity
    {
        public Guid Id { get; private set; }
        public PaymentAuthorization PaymentAuthorization { get; private set; }
        public Money Money { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public PaymentCapture(PaymentAuthorization paymentAuthorization, Money money)
        {
            Id = Guid.NewGuid();
            PaymentAuthorization = paymentAuthorization;
            Money = money;
            CreatedDate = new DateTime();
        }
        public void Validate(IValidator validator)
        {
            bool isAuthorizationNull = false;

            validator.AssertNotNull(PaymentAuthorization, "Capture must have an authorization", out isAuthorizationNull);
            Money.Validate(validator);

            if(!isAuthorizationNull)
            {
                validator.AssertGreaterOrEqualThan(PaymentAuthorization.Money.Amount, Money.Amount, "Amount", "Amount to be captured cannot be greater than the authorized amount");
                validator.AssertEquality(Money.Currency.CurrencyCode, PaymentAuthorization.Money.Currency.CurrencyCode, "Currency", "Capture operation must have the same currency of the given authorization");
                validator.AssertTrue(!PaymentAuthorization.WasRefunded, "Refund", "Once a refund has occurred, a capture cannot be made on the same authorization.");
                validator.AssertCustom(PaymentAuthorization.CreditCard.Number.ToString().Trim().Equals("4000000000000259"), "Credit card number", "4000 0000 0000 0259: capture failure");
            }
        }
    }
}
