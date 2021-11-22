using PaymentGateway.Application.Dtos;

namespace PaymentGateway.Application.Mappers
{
    
    public static class VoidMapper
    {
        public static Dtos.Response.VoidResponseDto ToVoidResponseDto(this Domain.ValueObjects.Money domain)
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