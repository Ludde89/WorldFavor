using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Entities;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Post_Duplicate_Reader : TestBase
    {
        private HttpResponseMessage _actual;
        private ReaderEntity _expectedReader;

        [TestMethod]
        public async Task Then_Expected_BadRequest_Should_Be_Returned()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, _actual.StatusCode);
            var message = await _actual.Content.ReadAsStringAsync();
            Assert.AreEqual(message, "Reader already exist");
        }

        protected override async Task Act()
        {
            Arrange();
            var client = TestServer.GetTestClient();
           
            var searlizedBook = JsonConvert.SerializeObject(_expectedReader);
            _actual = await client.PostAsync($"/api/readers/", new StringContent(searlizedBook, Encoding.UTF8, "application/json"));
        }

        private void Arrange()
        {
            var readers = DataHelper.GetReaders().ToList();
            _expectedReader = readers[3];
        }
    }
}