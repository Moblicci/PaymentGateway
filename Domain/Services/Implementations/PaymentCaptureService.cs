using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Service.Interfaces;
using PaymentGateway.Domain.Validations;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Domain.Service.Implementations
{
    public class PaymentCaptureService : IPaymentCaptureService
    {
        private readonly IValidator _validator;
        private readonly IPaymentAuthorizationService _authorizationService;
        private readonly IPaymentCaptureRepository _captureRepository;

        public PaymentCaptureService(IValidator validator, IPaymentAuthorizationService authorizationService, IPaymentCaptureRepository captureRepository)
        {
            _validator = validator;
            _authorizationService = authorizationService;
            _captureRepository =  captureRepository;
        }

        public Task<IEnumerable<PaymentCapture>> GetAllCaptures(Guid authorizationId) => _captureRepository.GetAllByAuthorization(authorizationId);

        public async Task<Money> NewCapture(PaymentCapture paymentCapture)
        {
            paymentCapture.Validate(_validator);
            if(!_validator.IsDomainValid())
                return default(Money);

            //Could be used a composite repository here in order to abstract this multiple writes strategy
            await _captureRepository.Insert(paymentCapture);
            await _captureRepository.InsertIndexedByAuthorization(paymentCapture);

            var capturedAuthorization = await _authorizationService.CaptureAuthorizedAmount(paymentCapture.PaymentAuthorization, paymentCapture.Money);

            return capturedAuthorization.Money;
        }

        public async Task<Money> Refund(PaymentRefund paymentRefund)
        {
            paymentRefund.Validate(_validator);
            if(!_validator.IsDomainValid())
                return default(Money);

            var refundedAuthorization = await _authorizationService.RefundAuthorizedAmount(paymentRefund.PaymentAuthorization, paymentRefund.Money);

            return refundedAuthorization.Money;
        }
    }
}