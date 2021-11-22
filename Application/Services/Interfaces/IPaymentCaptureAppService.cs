using System;
using System.Threading.Tasks;
using PaymentGateway.Application.Dtos.Request;
using PaymentGateway.Application.Dtos.Response;

namespace PaymentGateway.Application.Services.Interfaces
{
    public interface IPaymentCaptureAppService
    {
        Task<CaptureResponseDto> CreateCapture(CaptureRequestDto captureRequestDto);

        Task<RefundResponseDto> RefundCapture(RefundRequestDto refundRequestDto);
    }
}