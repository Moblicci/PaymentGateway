namespace PaymentGateway.Application.Dtos.Request
{
    public class AuthorizeRequestDto
    {
        public CreditCardDto CreditCard { get; set; }
        public MoneyDto Amount { get; set; }
    }
}