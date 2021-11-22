using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Repositories
{
    public interface IPaymentAuthorizationRepository : IRepository<PaymentAuthorization>
    {
    }
}