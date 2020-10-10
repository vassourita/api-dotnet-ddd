using Api.Data.Context;
using Api.Domain.Entities;

namespace Api.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository(MyContext context) : base(context) { }
    }
}