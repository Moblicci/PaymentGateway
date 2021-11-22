namespace PaymentGateway.Application.Services.Implementations
{
    using System;
    using System.Threading.Tasks;
    using PaymentGateway.Application.Dtos.Request;
    using PaymentGateway.Application.Dtos.Response;
    using PaymentGateway.Application.Mappers;
    using PaymentGateway.Application.Services.Interfaces;

    public class AuthorizationAppService : IAuthorizationAppService
    {

        private readonly Domain.Service.Interfaces.IPaymentAuthorizationService _authorizationService;

        public AuthorizationAppService(Domain.Service.Interfaces.IPaymentAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public async Task<AuthorizeResponseDto> CreateAuthorization(AuthorizeRequestDto authorizeRequest)
        {
            var domainResponse = await _authorizationService.CreateAuthorization(authorizeRequest.ToAuthorizationDomain());

            return domainResponse.ToAuthorizeResponseDto();
        }
        public async Task<AuthorizeResponseDto> GetAuthorization(Guid id)
        {
            var domainResponse = await _authorizationService.GetAuthorization(id);

            return domainResponse.ToAuthorizeResponseDto();
        }

        public async Task<VoidResponseDto> CancelAuthorization(Guid id)
        {
            var domainResponse = await _authorizationService.CancelAuthorization(id);

            return domainResponse.ToVoidResponseDto();
        }
    }
}