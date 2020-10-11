using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class TestBase
    {
        public TestBase()
        {

        }
    }

    public class DatabaseTest : IDisposable
    {
        private string DatabaseName = $"dbApiTest {Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; private set; }

        public DatabaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(opt =>
                opt.UseMySql($"Persist Security Info=True;Server=localhost;Database={DatabaseName};User=root;Password=change@123"), ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
