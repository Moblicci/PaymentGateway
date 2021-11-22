using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMemoryCache _memoryCache;

        public Repository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetById(Guid id)
        {
            try
            {
                T cacheEntry = default(T);

                await Task.Run(() => cacheEntry = (T)_memoryCache.Get(string.Concat(nameof(T), id)));

                return cacheEntry;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Insert(T data)
        {
            try
            {
                await Task.Run(() => 
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _memoryCache.Set(string.Concat(nameof(T), data.Id), data, cacheEntryOptions);
                });
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                 await Task.Run(() =>
                 {
                     _memoryCache.Remove(string.Concat(nameof(T), id));
                 });
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task Update(Guid id, T data)
        {
            try
            {
                await this.Delete(id);
                await this.Insert(data);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}