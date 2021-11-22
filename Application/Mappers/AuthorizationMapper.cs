using PaymentGateway.Application.Dtos;

namespace PaymentGateway.Application.Mappers
{
    
    public static class AuthorizationMapper
    {

        public static Dtos.Response.AuthorizeResponseDto ToAuthorizeResponseDto(this Domain.Entities.PaymentAuthorization domain) 
        {
            if(domain is null)
                return default(Dtos.Response.AuthorizeResponseDto);

            return new Dtos.Response.AuthorizeResponseDto(){
                        Id = domain.Id, 
                        AvailableAmount = new Dtos.MoneyDto(){
                            Amount = domain.Money.Amount.ToString(), 
                            Currency = domain.Money.Currency.CurrencyCode}};
        }

        public static Domain.Entities.PaymentAuthorization ToAuthorizationDomain(this Dtos.Request.AuthorizeRequestDto dto) 
            => new Domain.Entities.PaymentAuthorization(dto.CreditCard.ToCreditCardDomain(), dto.Amount.ToMoneyDomain());

        public static Dtos.Response.VoidResponseDto ToResponseDto(this Domain.ValueObjects.Money domain)
        {
            return domain is null 
                ? default(Dtos.Response.VoidResponseDto)
                : new Dtos.Response.VoidResponseDto(){
                    Amount = new MoneyDto(){
                        Currency = domain.Currency.CurrencyCode, 
                        Amount = domain.Amount.ToString()
                    }
                };
        }
    }
}