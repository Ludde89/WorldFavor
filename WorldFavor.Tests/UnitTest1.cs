using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorldFavor.Tests
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        private HttpResponseMessage _actual;

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.OK);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            _actual = await client.GetAsync("/api/books/foo");
        }
    }

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
                        .ConfigureServices(SetUpDependencies);
                });

            TestServer = hostBuilder.Build();
            TestServer.Start();
        }

        protected virtual void SetUpDependencies(IServiceCollection services) { }

        protected abstract Task Act();
    }
}
