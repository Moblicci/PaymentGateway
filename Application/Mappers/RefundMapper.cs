using System.Collections.Generic;
using PaymentGateway.Application.Dtos;

namespace PaymentGateway.Application.Mappers
{
    
    public static class RefundMapper
    {
        public static Domain.Entities.PaymentRefund ToRefundDomain(this Dtos.Request.RefundRequestDto requestDto, Domain.Entities.PaymentAuthorization paymentAuthorization, IEnumerable<Domain.Entities.PaymentCapture> paymentCaptures)
        {
            return new Domain.Entities.PaymentRefund(paymentAuthorization, requestDto.Money.ToMoneyDomain(), paymentCaptures);
        }
    }
}