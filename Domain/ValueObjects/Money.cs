using System;
using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.ValueObjects {
    public class Money : IValueObject<Money>
    {
        public Decimal Amount { get;}
        public Currency Currency { get;}

        public Money(Decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public Money(string amount, Currency currency)
        {
            this.Amount = Decimal.Parse(amount);
            this.Currency = currency;
        }

        public Money Add(Money moneyAdd)
        {
            return new Money(Amount + moneyAdd.Amount, Currency);
        }

        public Money Subtract(Money moneySubtract)
        {
            return new Money(Amount - moneySubtract.Amount, Currency);
        }

        public static Money FromDecimal(decimal amount, Currency currency)
            => new Money(amount, currency);

        public static Money FromString(string amount, Currency currency)
            => new Money(decimal.Parse(amount), currency);

        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);

        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

        public decimal ToDecimal()
            => Amount;

        public void Validate(IValidator validator)
        {
            Currency.Validate(validator);
            validator.AssertPositive(Amount, "Amount", "The amount value must be greater than 0");
        }
    }
}