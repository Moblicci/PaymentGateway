namespace PaymentGateway.Application.Dtos
{
    public class CreditCardDto
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public short ExpirationMonth { get; set; }
        public short ExpirationYear { get; set; }
        public string CVV { get; set; }
    }
}