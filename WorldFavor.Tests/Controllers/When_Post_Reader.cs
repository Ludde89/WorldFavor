using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Persistence.DbContext;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Post_Reader : TestBase
    {
        private HttpResponseMessage _actual;
        private Reader _expectedReader;

        [TestMethod]
        public async Task Then_Expected_Reader_Should_Be_Created()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.Created);

            await using var context = new WorldFavorDbContext(ContextOptionsBuilder.Options);

            var actualReader = await context.Readers.FirstOrDefaultAsync(x => x.Name == _expectedReader.Name && x.Birth == _expectedReader.Birth);

            Assert.AreEqual(_expectedReader.Name, actualReader.Name);
            Assert.AreEqual(_expectedReader.Birth, actualReader.Birth);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            var searlizedReader = JsonConvert.SerializeObject(_expectedReader);
            _actual = await client.PostAsync($"/api/readers/", new StringContent(searlizedReader, Encoding.UTF8, "application/json"));
        }

        protected override void Arrange()
        {
            _expectedReader = new Reader
            {
                Name = "foo",
                Birth = DateTime.Now
            };
        }
    }
}