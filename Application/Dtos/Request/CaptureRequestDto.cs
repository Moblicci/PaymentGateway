using System;

namespace PaymentGateway.Application.Dtos.Request
{
    public class CaptureRequestDto
    {
        public Guid AuthorizationId { get; set; }
        public MoneyDto Money { get; set; }
    }
}