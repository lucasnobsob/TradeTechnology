using Domain.Entities;
using Shared.ViewModels.User;

namespace Domain.Ports
{
    public interface IUserService
    {
        Task<IEnumerable<UserView>> GetAsync();
        Task<UserView> GetAsync(string login);
        Task<UserView> InsertAsync(NewUser novoUsuario);
        Task<LoggedInUser> ValidateUserAndGenerateTokenAsync(User user);
    }
}
