using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class RepositoryContainer
    {
        public static void ConfigureRepositoryDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            serviceCollection.AddDbContext<MyContext>(
                opt => opt.UseMySql("Server=localhost;Port=4001;Database=api_dotnet_ddd;Uid=root;Pwd=docker")
            );
        }
    }
}