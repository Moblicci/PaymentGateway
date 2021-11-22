namespace PaymentGateway.Domain.Entities
{
    using System;
    using Domain.ValueObjects;
    using PaymentGateway.Domain.Validations;

    public class PaymentAuthorization : IEntity
    {
        public Guid Id { get; private set; }
        public CreditCard CreditCard { get; private set; }
        public Money Money { get; private set; }
        public bool WasRefunded { get; private set; }
        public Decimal RefundLimit { get; private set; }

        public PaymentAuthorization(CreditCard creditCard, Money money)
        {
            Id = Guid.NewGuid();
            CreditCard = creditCard;
            Money = money;
            WasRefunded = false;
        }

        public PaymentAuthorization(Guid id, CreditCard creditCard, Money money, bool wasRefunded)
        {
            Id = id;
            CreditCard = creditCard;
            Money = money;
            WasRefunded = wasRefunded;
        }

        public void Validate(IValidator validator)
        {
            validator.AssertGuid(Id, "Authorization ID", "Cannot be empty");
            validator.AssertCustom(CreditCard.Number.ToString().Trim().Equals("4000000000000119"), "Credit card number", "4000 0000 0000 0119: authorise failure");
            CreditCard.Validate(validator);
            Money.Validate(validator);
        }

        public void IncreaseRefundLimit(Decimal amount)
        {
            if(amount > 0)
                RefundLimit += amount;
        }

        public void DecreaseRefundLimit(Decimal amount)
        {
            if(amount > 0)
                RefundLimit -= amount;
        }
    }
}
