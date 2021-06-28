using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Persistence.DbContext;

namespace WorldFavor.Tests
{
    public abstract class TestBase
    {
        protected IHost TestServer;

        [TestInitialize]
        public async Task Run()
        {
            SetUpDependencies();
            await Act();
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

            context.SaveChanges();
        }

        public DbContextOptionsBuilder<WorldFavorDbContext> ContextOptionsBuilder { get; set; }

        protected virtual void SetUpDependencies(IServiceCollection services) { }

        protected abstract Task Act();
    }

    public static class DataHelper
    {
        private static List<BookEntity> _books = new List<BookEntity>();
        private static List<ReaderEntity> _readers = new List<ReaderEntity>();
        public static IEnumerable<BookEntity> GetBooks()
        {
            if (!_books.Any())
            {
                _books.AddRange(Enumerable.Range(0, 5).Select(x => new BookEntity
                {
                    Checkout = DateTime.Now,
                    ISBN = $"foo{x}",
                    Id = x,
                    IsLost = false,
                    Title = $"bar{x}"
                }));
            }

            return _books;
        }

        public static IEnumerable<ReaderEntity> GetReaders()
        {
            if (!_readers.Any())
            {
                _readers.AddRange(Enumerable.Range(0, 5).Select(x => new ReaderEntity
                {
                  Id = x,
                  Name = $"foobar{x}",
                  Birth = DateTime.Now.AddDays(-x)
                }));
            }

            return _readers;
        }
    }
}