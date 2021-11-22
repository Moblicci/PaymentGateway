namespace PaymentGateway.Application.Services.Implementations
{
    using System.Threading.Tasks;
    using PaymentGateway.Application.Dtos.Request;
    using PaymentGateway.Application.Dtos.Response;
    using PaymentGateway.Application.Mappers;
    using PaymentGateway.Application.Services.Interfaces;

    public class PaymentCaptureAppService : IPaymentCaptureAppService
    {

        private readonly Domain.Service.Interfaces.IPaymentCaptureService _paymentCaptureService;
        private readonly Domain.Service.Interfaces.IPaymentAuthorizationService _paymentAuthorizationService;

        public PaymentCaptureAppService(Domain.Service.Interfaces.IPaymentCaptureService paymentCaptureService, Domain.Service.Interfaces.IPaymentAuthorizationService paymentAuthorizationService)
        {
            _paymentCaptureService = paymentCaptureService;
            _paymentAuthorizationService = paymentAuthorizationService;
        }

        public async Task<CaptureResponseDto> CreateCapture(CaptureRequestDto captureRequestDto)
        {
            var authorization = await _paymentAuthorizationService.GetAuthorization(captureRequestDto.AuthorizationId);
            var domainResponse = await _paymentCaptureService.NewCapture(captureRequestDto.ToCaptureDomain(authorization));
            
            if(domainResponse is null)
                return default(CaptureResponseDto);

            var response = new CaptureResponseDto(){Money = domainResponse.ToMoneyResponseDto()};
            return response;
        }

        public async Task<RefundResponseDto> RefundCapture(RefundRequestDto refundRequestDto)
        {
            var paymentAuthorization = await _paymentAuthorizationService.GetAuthorization(refundRequestDto.AuthorizationId);
            var captureLst = await _paymentCaptureService.GetAllCaptures(refundRequestDto.AuthorizationId);

            var domainResponse = await _paymentCaptureService.Refund(refundRequestDto.ToRefundDomain(paymentAuthorization, captureLst));
            
            if(domainResponse is null)
                return default(RefundResponseDto);

            var response = new RefundResponseDto(){Money = domainResponse.ToMoneyResponseDto()};

            return response;
        }
    }
}