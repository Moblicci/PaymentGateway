using System;
using System.Threading.Tasks;
using PaymentGateway.Application.Dtos.Request;
using PaymentGateway.Application.Dtos.Response;

namespace PaymentGateway.Application.Services.Interfaces
{
    public interface IAuthorizationAppService
    {
        Task<AuthorizeResponseDto> CreateAuthorization(AuthorizeRequestDto authorizeRequest);
        Task<AuthorizeResponseDto> GetAuthorization(Guid id);
        Task<VoidResponseDto> CancelAuthorization(Guid id);
    }
}