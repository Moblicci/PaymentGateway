namespace PaymentGateway.Application.Mappers
{    
    public static class ValueObjectsMapper
    {
        public static Domain.ValueObjects.CreditCard ToCreditCardDomain(this Dtos.CreditCardDto dto)
        {
            var cardNumber = new Domain.ValueObjects.CardNumber(dto.CardNumber);
            var cvv = new Domain.ValueObjects.CVV(dto.CVV);

            return new Domain.ValueObjects.CreditCard(
                cardNumber, 
                dto.CardHolderName, 
                new System.DateTime(dto.ExpirationYear, dto.ExpirationMonth, 1), 
                cvv);
        }

        public static Domain.ValueObjects.Money ToMoneyDomain(this Dtos.MoneyDto dto)
        {
            var currency = new Domain.ValueObjects.Currency(dto.Currency, 2);

            return Domain.ValueObjects.Money.FromString(dto.Amount, currency);
        }

        public static Dtos.MoneyDto ToMoneyResponseDto(this Domain.ValueObjects.Money dto)
        {
            return new Dtos.MoneyDto(){Amount = dto.Amount.ToString(), Currency = dto.Currency.CurrencyCode};
        }
    }
}