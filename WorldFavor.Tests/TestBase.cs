using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Persistence.DbContext;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests
{
    public abstract class TestBase
    {
        protected IHost TestServer;

        [TestInitialize]
        public async Task Run()
        {
            Arrange();
            SetUpDependencies();
            await Act();
        }

        protected virtual void Arrange()
        {
        }

        private void SetUpDependencies()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer()
                        .UseStartup<Startup>()
                        .ConfigureServices(services =>
                        {
                            SetUpDependencies(services);
                            services.AddSingleton(p =>
                            {
                                ContextOptionsBuilder = new DbContextOptionsBuilder<WorldFavorDbContext>();
                                ContextOptionsBuilder.UseInMemoryDatabase($"WF_{Guid.NewGuid()}");
                                var dbContext = new WorldFavorDbContext(ContextOptionsBuilder.Options);
                                SetupDatabaseWithData();
                                return dbContext;
                            });
                        });

                    
                });

            TestServer = hostBuilder.Build();
            TestServer.Start();
        }

        private void SetupDatabaseWithData()
        {
            using var context = new WorldFavorDbContext(ContextOptionsBuilder.Options);
            SeedDataBase(context);
        }

        private void SeedDataBase(WorldFavorDbContext context)
        {
            var bookEntities = DataHelper.GetBooks();
            context.Books.AddRange(bookEntities);

            var readerEntities = DataHelper.GetReaders();
            context.Readers.AddRange(readerEntities);

            SeedDatabase(context);
            context.SaveChanges();
        }

        public DbContextOptionsBuilder<WorldFavorDbContext> ContextOptionsBuilder { get; set; }

        protected virtual void SetUpDependencies(IServiceCollection services) { }
        protected virtual void SeedDatabase(WorldFavorDbContext context) { }

        protected abstract Task Act();
    }
}