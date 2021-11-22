using System;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task Insert(T data);
        Task<T> GetById(Guid id);
        Task Update(Guid id, T data);
        Task Delete(Guid id);
    }
}