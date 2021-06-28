using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                        .ConfigureServices(SetUpDependencies);
                });

            TestServer = hostBuilder.Build();
            TestServer.Start();
        }

        protected virtual void SetUpDependencies(IServiceCollection services) { }

        protected abstract Task Act();
    }
}