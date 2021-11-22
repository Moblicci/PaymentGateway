namespace PaymentGateway.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.ValueObjects;
    using PaymentGateway.Domain.Validations;

    public class PaymentRefund : IEntity
    {
        public Guid Id { get; private set; }
        public PaymentAuthorization PaymentAuthorization { get; private set; }
        public IEnumerable<PaymentCapture> Captures { get; set; }
        public Money Money { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public Decimal TotalCaptured { get; set; }

        public PaymentRefund(PaymentAuthorization paymentAuthorization, Money money, IEnumerable<PaymentCapture> captures)
        {
            Id = Guid.NewGuid();
            PaymentAuthorization = paymentAuthorization;
            Money = money;
            Captures = captures;
            CreatedDate = new DateTime();
            TotalCaptured = captures.ToList().Sum(c => c.Money.Amount);
        }
        public void Validate(IValidator validator)
        {
            bool isAuthorizationNull = false;

            validator.AssertNotNull(PaymentAuthorization, "Refund must have an authorization", out isAuthorizationNull);
            Money.Validate(validator);

            if(!isAuthorizationNull)
            {
                validator.AssertGreaterOrEqualThan(TotalCaptured, Money.Amount, "Amount", "Amount to be refunded cannot be greater than the total captured");
                validator.AssertGreaterOrEqualThan(PaymentAuthorization.RefundLimit, Money.Amount, "Amount", "Amount to be refunded cannot be greater than the total captured");
                validator.AssertEquality(Money.Currency.CurrencyCode, PaymentAuthorization.Money.Currency.CurrencyCode, "Currency", "Capture operation must have the same currency of the given authorization");
                validator.AssertCustom(PaymentAuthorization.CreditCard.Number.ToString().Trim().Equals("4000000000003238"), "Credit card number", "4000 0000 0000 3238: refund failure");
            }
        }
    }
}
