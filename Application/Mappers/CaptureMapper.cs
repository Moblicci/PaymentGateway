using PaymentGateway.Application.Dtos;
using PaymentGateway.Application.Dtos.Request;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Application.Mappers
{
    
    public static class CaptureMapper
    {
        public static PaymentCapture ToCaptureDomain(this CaptureRequestDto requestDto, PaymentAuthorization paymentAuthorization)
        {
            var money = Money.FromString(requestDto.Money.Amount, new Currency(requestDto.Money.Currency, 2));

            return new PaymentCapture(paymentAuthorization, money);
        }
    }
}