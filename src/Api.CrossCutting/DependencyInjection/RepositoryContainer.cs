using System;
using Api.Data.Context;
using Api.Data.Repositories;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class RepositoryContainer
    {
        public static void ConfigureRepositoryDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            if (Environment.GetEnvironmentVariable("DB_DIALECT").ToLower() == "mysql")
            {
                serviceCollection.AddDbContext<MyContext>(
                    opt => opt.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                );
            }
            else
            {
                throw new NotImplementedException("Only MySQL Databases are configured on this project");
            }
        }
    }
}