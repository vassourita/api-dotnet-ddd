using System.Threading.Tasks;
using Api.Domain.DTOs;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<object> Login(LoginDTO loginInfo);
    }
}