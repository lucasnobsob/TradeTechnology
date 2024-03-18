using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Ports
{
    public interface ICustomerRepository
    {
        Task Add(Customer entity);
        Task AddAll(IEnumerable<Customer> entity);
        Task<IEnumerable<Customer>> Find(Expression<Func<Customer, bool>> predicate);
        Task<IEnumerable<Customer>> GetAll();
        Task Remove(Expression<Func<Customer, bool>> predicate);
        Task<Customer> Update(Expression<Func<Customer, bool>> predicate, Customer entity);
    }
}
