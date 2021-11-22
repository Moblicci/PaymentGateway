using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Dtos.Request;
using PaymentGateway.Application.Dtos.Response;
using PaymentGateway.Application.Services.Interfaces;
using PaymentGateway.Domain.Notifications;

namespace PaymentGateway.Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly IAuthorizationAppService _authorizationService;
        private INotificationHandler _notificationHandler;

        public AuthorizeController(
            ILogger<AuthorizeController> logger, 
            IAuthorizationAppService authorizationService, 
            INotificationHandler notificationHandler)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _notificationHandler = notificationHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorizeResponseDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> PostAsync([FromBody] AuthorizeRequestDto input)
        {
            var output = await _authorizationService.CreateAuthorization(input);

            //TODO 3#
            if (_notificationHandler.HasNotifications())
            {
                _logger.LogWarning("Authorization has notifications", _notificationHandler.Notify());//TODO 2#
                return BadRequest(new { success = false, errors = _notificationHandler.Notify() });
            }

            return this.CreatedAtRoute("GetAuthorized", new { authorizationId = output.Id }, output);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AuthorizeResponseDto), (int)HttpStatusCode.OK)]
        [Route("{authorizationId:guid}", Name = "GetAuthorized")]
        public async Task<IActionResult> GetAuthorizedAsync([FromRoute]Guid authorizationId)
        {
            var output = await _authorizationService.GetAuthorization(authorizationId);

            //TODO 3#
            if(output == default(AuthorizeResponseDto))
                return NotFound(new { success = false, errors = "Authorization ID does not exist" });

            return Ok(output);
        }
    }
}
