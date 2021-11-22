namespace PaymentGateway.Domain.Service.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PaymentGateway.Domain.Entities;
    using PaymentGateway.Domain.ValueObjects;

    public interface IPaymentCaptureService
    {
        Task<Money> NewCapture(PaymentCapture paymentCapture);

        Task<IEnumerable<PaymentCapture>> GetAllCaptures(Guid authorizationId);

        Task<Money> Refund(PaymentRefund paymentRefund);
    }
}