using System;

namespace PaymentGateway.Application.Dtos.Response
{
    public class AuthorizeResponseDto
    {
        public Guid Id { get; set; }
        public MoneyDto AvailableAmount { get; set; }
    }
}