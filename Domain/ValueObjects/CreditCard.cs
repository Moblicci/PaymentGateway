using System;
using PaymentGateway.Domain.Notifications;
using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects
{
    public class CreditCard : IValueObject<CreditCard>
    {
        public CardNumber Number { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public CVV CVV { get; set; }

        public CreditCard(CardNumber number, string cardHolderName, DateTime expirationDate, CVV cvv)
        {
            this.Number = number;
            this.CardHolderName = cardHolderName;
            this.ExpirationDate = expirationDate;
            this.CVV = cvv;
        }

        public void Validate(IValidator validator)
        {
            Number.Validate(validator);
            CVV.Validate(validator);
        }
    }
}
