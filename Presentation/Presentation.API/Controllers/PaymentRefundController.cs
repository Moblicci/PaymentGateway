using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Dtos.Request;
using PaymentGateway.Application.Dtos.Response;
using PaymentGateway.Application.Services.Interfaces;
using PaymentGateway.Domain.Notifications;

namespace PaymentGateway.Presentation.API.Controllers
{
    [ApiController]
    [Route("refund")]
    public class PaymentRefundController : ControllerBase
    {
        private readonly IPaymentCaptureAppService _captureService;
        private INotificationHandler _notificationHandler;

        public PaymentRefundController(
            IPaymentCaptureAppService captureService, 
            INotificationHandler notificationHandler)
        {
            _captureService = captureService;
            _notificationHandler = notificationHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RefundResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromBody]RefundRequestDto requestDto)
        {
            var result = await _captureService.RefundCapture(requestDto);

            if (_notificationHandler.HasNotifications())
                return BadRequest(new { success = false, errors = _notificationHandler.Notify() });

            if(result == default(RefundResponseDto))
                return NotFound(new { success = false, errors = "Authorization ID does not exist" });

            return this.Ok(result);
        }
    }
}