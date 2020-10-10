using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _Repository;

        public UserService(IRepository<UserEntity> repository)
        {
            _Repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _Repository.DeleteAsync(id);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await _Repository.SelectAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _Repository.SelectAsync();
        }

        public async Task<UserEntity> Post(UserEntity user)
        {
            return await _Repository.InsertAsync(user);
        }

        public async Task<UserEntity> Put(UserEntity user)
        {
            return await _Repository.UpdateAsync(user);
        }
    }
}