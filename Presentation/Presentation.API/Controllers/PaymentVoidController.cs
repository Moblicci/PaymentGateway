using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Dtos.Response;
using PaymentGateway.Application.Services.Interfaces;
using PaymentGateway.Domain.Notifications;

namespace PaymentGateway.Presentation.API.Controllers
{
    [ApiController]
    [Route("void")]
    public class PaymentVoidController : ControllerBase
    {
        private readonly IAuthorizationAppService _authorizationService;
        private INotificationHandler _notificationHandler;

        public PaymentVoidController(
            IAuthorizationAppService authorizationService, 
            INotificationHandler notificationHandler)
        {
            _authorizationService = authorizationService;
            _notificationHandler = notificationHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(VoidResponseDto), (int)HttpStatusCode.OK)]
        [Route("{authorizationId:guid}", Name = "Void")]
        public async Task<IActionResult> PostAsync([FromRoute]Guid authorizationId)
        {
            var result = await _authorizationService.CancelAuthorization(authorizationId);

            if (_notificationHandler.HasNotifications())
                return BadRequest(new { success = false, errors = _notificationHandler.Notify() });

            if(result == default(VoidResponseDto))
                return NotFound(new { success = false, errors = "Authorization ID does not exist" });

            return this.Ok(result);
        }
    }
}