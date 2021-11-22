using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Domain.Service.Interfaces
{
    public interface IPaymentAuthorizationService
    {
        Task<PaymentAuthorization> CreateAuthorization(PaymentAuthorization authorization);

        Task<PaymentAuthorization> GetAuthorization(Guid id);

        Task<PaymentAuthorization> CaptureAuthorizedAmount(PaymentAuthorization authorization, Money money);

        Task<PaymentAuthorization> RefundAuthorizedAmount(PaymentAuthorization authorization, Money money);

        Task<Money> CancelAuthorization(Guid id);
    }
}