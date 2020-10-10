using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _DataSet;
        public UserRepository(MyContext context) : base(context)
        {
            _DataSet = _Context.Set<UserEntity>();
        }

        public async Task<UserEntity> SelectAsync(string email)
        {
            try
            {
                return await _DataSet.SingleOrDefaultAsync(x => x.Email.Equals(email));
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}