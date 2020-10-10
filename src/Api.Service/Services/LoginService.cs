using System.Threading.Tasks;
using Api.Domain.DTOs;
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
        public async Task<object> Login(LoginDTO loginInfo)
        {
            if (loginInfo == null || string.IsNullOrWhiteSpace(loginInfo.Email))
                return null;

            return await _Repository.SelectByEmailAsync(loginInfo.Email);
        }
    }
}