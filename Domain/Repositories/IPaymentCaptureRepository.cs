using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Repositories
{
    public interface IPaymentCaptureRepository : IRepository<PaymentCapture>
    {
        Task InsertIndexedByAuthorization(PaymentCapture data);

        Task<IEnumerable<PaymentCapture>> GetAllByAuthorization(Guid authorizationId);
    }
}