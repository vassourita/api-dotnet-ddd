using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _Repository;

        public LoginService(IUserRepository repository)
        {
            _Repository = repository;
        }
        public async Task<object> Login(UserEntity user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
                return null;

            return await _Repository.SelectByEmailAsync(user.Email);
        }
    }
}