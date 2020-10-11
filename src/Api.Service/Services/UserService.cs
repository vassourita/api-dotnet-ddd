using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _Repository;

        private readonly IMapper _Mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _Mapper = mapper;
        }

        public async Task<bool> Delete(Guid id) => await _Repository.DeleteAsync(id);

        public async Task<UserDTO> GetByEmail(string email)
        {
            var entity = await _Repository.SelectByEmailAsync(email);
            return _Mapper.Map<UserDTO>(entity);
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var entity = await _Repository.SelectAsync(id);
            return _Mapper.Map<UserDTO>(entity);
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var entities = await _Repository.SelectAsync();
            return _Mapper.Map<IEnumerable<UserDTO>>(entities);
        }

        public async Task<UserDTOCreateResult> Create(UserDTO userInfo)
        {
            var userExists = await _Repository.ExistAsync(u => u.Email == userInfo.Email);
            if (userExists)
            {
                return null;
            }

            var model = _Mapper.Map<UserModel>(userInfo);
            var entity = _Mapper.Map<UserEntity>(model);
            var result = await _Repository.InsertAsync(entity);

            return _Mapper.Map<UserDTOCreateResult>(result);
        }

        public async Task<UserDTOUpdateResult> Update(UserDTO userInfo)
        {
            var model = _Mapper.Map<UserModel>(userInfo);
            var entity = _Mapper.Map<UserEntity>(model);
            var result = await _Repository.UpdateAsync(entity);

            return _Mapper.Map<UserDTOUpdateResult>(result);
        }

    }
}