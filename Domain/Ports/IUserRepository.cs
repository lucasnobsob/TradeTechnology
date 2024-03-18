using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<User> Add(User entity);
        Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> GetAll();
        Task Remove(Expression<Func<User, bool>> predicate);
        Task<User> Update(Expression<Func<User, bool>> predicate, User entity);
    }
}
