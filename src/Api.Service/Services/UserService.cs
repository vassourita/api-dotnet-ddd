using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _Repository;

        public UserService(IUserRepository repository) => _Repository = repository;

        public async Task<bool> Delete(Guid id) => await _Repository.DeleteAsync(id);

        public async Task<UserEntity> GetByEmail(string email) => await _Repository.SelectByEmailAsync(email);

        public async Task<UserEntity> GetById(Guid id) => await _Repository.SelectAsync(id);

        public async Task<IEnumerable<UserEntity>> GetAll() => await _Repository.SelectAsync();

        public async Task<UserEntity> Create(UserEntity user)
        {
            var userExists = await _Repository.ExistAsync(u => u.Email == user.Email);
            if (userExists)
            {
                return null;
            }

            return await _Repository.InsertAsync(user);
        }

        public async Task<UserEntity> Update(UserEntity user) => await _Repository.UpdateAsync(user);

    }
}