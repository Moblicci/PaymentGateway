using System;

namespace PaymentGateway.Application.Dtos.Request
{
    public class VoidRequestDto
    {
        public Guid AuthorizationId { get; set; }
    }
}