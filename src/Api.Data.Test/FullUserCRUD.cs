using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repositories;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class FullUserCRUD : TestBase, IClassFixture<DatabaseTest>
    {
        private ServiceProvider _ServiceProvider;

        public FullUserCRUD(DatabaseTest databaseTest)
        {
            _ServiceProvider = databaseTest.ServiceProvider;
        }

        [Fact(DisplayName = "UserCRUD")]
        [Trait("CRUD", "UserEntity")]
        public async Task CanRealizeUserCRUD()
        {
            using (var context = _ServiceProvider.GetService<MyContext>())
            {
                IUserRepository repository = new UserRepository(context);

                var user = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var created = await repository.InsertAsync(user);

                Assert.NotNull(created);
                Assert.NotNull(created.CreatedAt);
                Assert.Equal(user.Name, created.Name);
                Assert.Equal(user.Email, created.Email);
                Assert.NotEqual(created.Id, Guid.Empty);

                user.Name = Faker.Name.First();

                var updated = await repository.UpdateAsync(user);
                Assert.NotNull(created);
                Assert.NotNull(updated.CreatedAt);
                Assert.NotNull(updated.UpdatedAt);
                Assert.Equal(user.Name, updated.Name);
                Assert.Equal(user.Email, updated.Email);
                Assert.NotEqual(updated.Id, Guid.Empty);

                var exists = await repository.ExistAsync(x => x.Id == user.Id);
                Assert.True(exists);

                var selected = await repository.SelectAsync(updated.Id);
                Assert.NotNull(selected);
                Assert.Equal(selected.Name, updated.Name);
                Assert.Equal(selected.Email, updated.Email);

                var selectedByEmail = await repository.SelectByEmailAsync(updated.Email);
                Assert.NotNull(selected);
                Assert.Equal(selected.Name, updated.Name);
                Assert.Equal(selected.Email, updated.Email);

                var all = await repository.SelectAsync();
                Assert.NotNull(all);
                Assert.True(all.Count() > 0);

                var deleted = await repository.DeleteAsync(updated.Id);
                Assert.True(deleted);
            }
        }
    }
}