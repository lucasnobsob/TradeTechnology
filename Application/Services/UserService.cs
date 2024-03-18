using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Identity;
using Shared.ViewModels.User;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IJWTService jwtService;

        public UserService(IUserRepository repository, IMapper mapper, IJWTService jwtService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.jwtService = jwtService;
        }

        public async Task<IEnumerable<UserView>> GetAsync()
        {
            return mapper.Map<IEnumerable<User>, IEnumerable<UserView>>(await repository.GetAll());
        }

        public async Task<UserView> GetAsync(string login)
        {
            return mapper.Map<UserView>(await repository.Find(x => x.Login == login));
        }

        public async Task<UserView> InsertAsync(NewUser novoUsuario)
        {
            var user = mapper.Map<User>(novoUsuario);
            ConvertPasswordToHash(user);
            return mapper.Map<UserView>(await repository.Add(user));
        }

        public async Task<LoggedInUser> ValidateUserAndGenerateTokenAsync(User user)
        {
            var usersFound = await repository.Find(x => x.Login == user.Login);
            var userFound = usersFound.FirstOrDefault();
            if (userFound == null)
                return new LoggedInUser();

            if (await ValidateAndUpdateHashAsync(user, userFound.Password))
            {
                var usuarioLogado = mapper.Map<LoggedInUser>(userFound);
                usuarioLogado.Token = jwtService.GerarToken(userFound);
                return usuarioLogado;
            }
            return new LoggedInUser();
        }

        private void ConvertPasswordToHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        private async Task<bool> ValidateAndUpdateHashAsync(User usuario, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(usuario, hash, usuario.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
