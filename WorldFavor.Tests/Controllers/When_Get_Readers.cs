using System.Collections.Generic;
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
    public class When_Get_Readers : TestBase
    {
        private HttpResponseMessage _actual;
        private IEnumerable<ReaderEntity> _expectedReaders;

        [TestMethod]
        public async Task Then_Expected_Reader_Should_Be_Returned()
        {
            Assert.AreEqual(HttpStatusCode.OK, _actual.StatusCode);
            var actualReader = JsonConvert.DeserializeObject<IEnumerable<Reader>>(await _actual.Content.ReadAsStringAsync());

            var sortedActualReader = actualReader.OrderBy(x => x.Name).ToList();
            var sorteExpectedReaders = _expectedReaders.OrderBy(x => x.Name).ToList();

            for (int i = 0; i < sortedActualReader.Count; i++)
            {

                Assert.AreEqual(sorteExpectedReaders[i].Birth, sortedActualReader[i].Birth);
                Assert.AreEqual(sorteExpectedReaders[i].Name, sortedActualReader[i].Name);
            }
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            _actual = await client.GetAsync($"/api/readers/");
        }

        protected override void Arrange()
        {
            _expectedReaders = DataHelper.GetReaders().ToList();
        }
    }
}