using Microsoft.Extensions.Caching.Memory;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class PaymentAuthorizationRepository : Repository<PaymentAuthorization>, IPaymentAuthorizationRepository
    {

        public PaymentAuthorizationRepository(IMemoryCache memoryCache)
            :base(memoryCache)
        {
        }
    }
}