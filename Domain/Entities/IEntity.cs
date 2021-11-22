using System;
using PaymentGateway.Domain.Validations;

namespace PaymentGateway.Domain.Entities {
    public interface IEntity : IValidatable
    {
        Guid Id{get;}
    }
}