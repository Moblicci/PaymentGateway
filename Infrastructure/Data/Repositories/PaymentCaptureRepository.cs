using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class PaymentCaptureRepository : Repository<PaymentCapture>, IPaymentCaptureRepository
    {
        private readonly IMemoryCache _memoryCache;
        public PaymentCaptureRepository(IMemoryCache memoryCache)
            :base(memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<PaymentCapture>> GetAllByAuthorization(Guid authorizationId)
        {
            try
            {
                var captureLst = new List<PaymentCapture>();

                await Task.Run(() => captureLst = (List<PaymentCapture>)_memoryCache.Get(string.Concat(nameof(PaymentCapture), "authorizationIndexed", authorizationId)));

                return captureLst;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task InsertIndexedByAuthorization(PaymentCapture data)
        {
            try
            {
                await Task.Run(() => 
                {
                
                    List<PaymentCapture> captureLst = (List<PaymentCapture>)_memoryCache.Get(string.Concat(nameof(PaymentCapture), "authorizationIndexed", data.PaymentAuthorization.Id));

                    if(captureLst is null)
                        captureLst = new List<PaymentCapture>();

                    captureLst.Add(data);

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _memoryCache.Set(string.Concat(nameof(PaymentCapture), "authorizationIndexed", data.PaymentAuthorization.Id), captureLst, cacheEntryOptions);
                });
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}