using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Get_Reader : TestBase
    {
        private HttpResponseMessage _actual;
        private ReaderEntity _expectedReader;

        [TestMethod]
        public async Task Then_Expected_Reader_Should_Be_Returned()
        {
            Assert.AreEqual(HttpStatusCode.OK, _actual.StatusCode);
            var actualReader = JsonConvert.DeserializeObject<Reader>(await _actual.Content.ReadAsStringAsync());

            Assert.AreEqual(_expectedReader.Birth, actualReader.Birth);
            Assert.AreEqual(_expectedReader.Name, actualReader.Name);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            _actual = await client.GetAsync($"/api/readers/{_expectedReader.Name}");
        }

        protected override void Arrange()
        {
            _expectedReader = DataHelper.GetReaders().ToList()[1];
        }
    }
}
