using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetByEmail(string email);

        Task<UserDTO> GetById(Guid id);

        Task<IEnumerable<UserDTO>> GetAll();

        Task<UserDTOCreateResult> Create(UserDTO userInfo);

        Task<UserDTOUpdateResult> Update(UserDTO userInfo);

        Task<bool> Delete(Guid id);
    }
}