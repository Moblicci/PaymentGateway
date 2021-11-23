namespace DomainTests.Entities
{
    using System;
    using Xunit;
    using PaymentGateway.Domain.Entities;
    using PaymentGateway.Domain.ValueObjects;
    using PaymentGateway.Domain.Validations;
    using PaymentGateway.Domain.Notifications;
    using System.Linq;

    public class PaymentAuthorizationTests
    {

        [Fact]
        public void Validate_WithEmptyGuid_ShouldAddNotification()
        {
            //Arrange
            var paymentAuthorization = new PaymentAuthorization(Guid.Empty, CreateValidCreditCard(), CreateValidMoney(), false);
            var notificationHandler = new NotificationHandler();
            var validator = new Validator(notificationHandler);

            //Act
            paymentAuthorization.Validate(validator);
            var notificationContainer = notificationHandler.Notify();

            //Assert
            Assert.NotEmpty(notificationContainer);
            Assert.True(notificationContainer.ToList().Exists(n => n.Key == "Authorization ID"));

            notificationHandler.Dispose();
        }

        [Fact]
        public void Validate_WithInvalidCreditCardNumber_ShouldAddNotification()
        {
            //Arrange
            var paymentAuthorization = new PaymentAuthorization(CreateInvalidCreditCard(), CreateValidMoney());
            var notificationHandler = new NotificationHandler();
            var validator = new Validator(notificationHandler);

            //Act
            paymentAuthorization.Validate(validator);
            var notificationContainer = notificationHandler.Notify();

            //Assert
            Assert.NotEmpty(notificationContainer);
            Assert.True(notificationContainer.ToList().Exists(n => n.Key == "Credit card number"));

            notificationHandler.Dispose();
        }

        private static CreditCard CreateValidCreditCard() => new CreditCard(new CardNumber("123456789"), "Test User", DateTime.Now.AddYears(5), new CVV("123"));

        private static CreditCard CreateInvalidCreditCard() => new CreditCard(new CardNumber("4000000000000119"), "Test User", DateTime.Now.AddYears(5), new CVV("123"));

        private static Money CreateValidMoney() => new Money("500", new Currency() { CurrencyCode = "EUR", DecimalPlaces = 2});
    }
}
