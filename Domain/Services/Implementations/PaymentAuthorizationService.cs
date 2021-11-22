using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Service.Interfaces;
using PaymentGateway.Domain.Validations;
using PaymentGateway.Domain.ValueObjects;

namespace PaymentGateway.Domain.Service.Implementations
{
    public class PaymentAuthorizationService : IPaymentAuthorizationService
    {
        private readonly IValidator _validator;
        private readonly IPaymentAuthorizationRepository _authorizationRepository;

        public PaymentAuthorizationService(IValidator validator, IPaymentAuthorizationRepository authorizationRepository)
        {
            _validator = validator;
            _authorizationRepository = authorizationRepository;
        }

        public async Task<PaymentAuthorization> CreateAuthorization(PaymentAuthorization authorization)
        {
            authorization.Validate(_validator);
            if(_validator.IsDomainValid())
                await _authorizationRepository.Insert(authorization);

            return authorization;
        }

        public async Task<PaymentAuthorization> GetAuthorization(Guid id) => await _authorizationRepository.GetById(id);

        public async Task<Money> CancelAuthorization(Guid id)
        {
            try
            {
                var authorization = await this.GetAuthorization(id);
                var money = authorization?.Money;

                if(authorization is not null)
                    await _authorizationRepository.Delete(id);

                return money;    
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<PaymentAuthorization> CaptureAuthorizedAmount(PaymentAuthorization authorization, Money money)
        {
            var capturedAuthorization = new PaymentAuthorization(authorization.Id, authorization.CreditCard, authorization.Money.Subtract(money), authorization.WasRefunded);
            capturedAuthorization.IncreaseRefundLimit(money.Amount);

            capturedAuthorization.Validate(_validator);
            if(!_validator.IsDomainValid())
                return default(PaymentAuthorization);

            await _authorizationRepository.Update(authorization.Id, capturedAuthorization);

            return capturedAuthorization;
        }

        public async Task<PaymentAuthorization> RefundAuthorizedAmount(PaymentAuthorization authorization, Money money)
        {
            var refundedAuthorization = new PaymentAuthorization(authorization.Id, authorization.CreditCard, authorization.Money.Add(money), true);
            refundedAuthorization.DecreaseRefundLimit(money.Amount);

            refundedAuthorization.Validate(_validator);
            if(!_validator.IsDomainValid())
                return default(PaymentAuthorization);

            await _authorizationRepository.Update(authorization.Id, refundedAuthorization);

            return refundedAuthorization;
        }
    }
}