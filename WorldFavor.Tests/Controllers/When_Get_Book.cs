using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Controllers;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Get_Book : TestBase
    {
        private HttpResponseMessage _actual;

        [TestMethod]
        public async Task Then_Expected_Book_Should_Be_Returned()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.OK);
            var book = JsonConvert.DeserializeObject<Book>(await _actual.Content.ReadAsStringAsync());

            Assert.AreEqual(book.Title, "foo");
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            _actual = await client.GetAsync("/api/books/foo");
        }
    }
}
