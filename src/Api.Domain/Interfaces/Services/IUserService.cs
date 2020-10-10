using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetByEmail(string email);

        Task<UserEntity> GetById(Guid id);

        Task<IEnumerable<UserEntity>> GetAll();

        Task<UserEntity> Create(UserEntity user);

        Task<UserEntity> Update(UserEntity user);

        Task<bool> Delete(Guid id);
    }
}