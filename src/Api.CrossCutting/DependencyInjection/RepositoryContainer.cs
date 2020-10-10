using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class RepositoryContainer
    {
        public static void ConfigureRepositoryDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}