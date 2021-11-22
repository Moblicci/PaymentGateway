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
    [Route("capture")]
    public class PaymentCaptureController : ControllerBase
    {
        private readonly IPaymentCaptureAppService _paymentCaptureService;
        private INotificationHandler _notificationHandler;

        public PaymentCaptureController(
            IPaymentCaptureAppService paymentCaptureService, 
            INotificationHandler notificationHandler)
        {
            _paymentCaptureService = paymentCaptureService;
            _notificationHandler = notificationHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CaptureResponseDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync([FromBody]CaptureRequestDto request)
        {
            var result = await _paymentCaptureService.CreateCapture(request);

            if (_notificationHandler.HasNotifications())
                return BadRequest(new { success = false, errors = _notificationHandler.Notify() });

            if(result == default(CaptureResponseDto))
                return NotFound(new { success = false, errors = "Authorization ID does not exist" });

            return this.Ok(result);
        }
    }
}