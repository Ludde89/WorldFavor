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

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Get_Books : TestBase
    {
        private HttpResponseMessage _actual;
        private IEnumerable<BookEntity> _expectedBooks;

        [TestMethod]
        public async Task Then_Expected_Books_Should_Be_Returned()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.OK);
            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(await _actual.Content.ReadAsStringAsync());

            var sortedActualBooks = books.OrderBy(x => x.ISBN).ToList();
            var sortedExpectedBooks = _expectedBooks.OrderBy(x => x.ISBN).ToList();

            for (int i = 0; i < sortedActualBooks.Count; i++)
            {
                Assert.AreEqual(sortedExpectedBooks[i].Title, sortedActualBooks[i].Title);
                Assert.AreEqual(sortedExpectedBooks[i].Checkout, sortedActualBooks[i].Checkout);
                Assert.AreEqual(sortedExpectedBooks[i].ISBN, sortedActualBooks[i].ISBN);
                Assert.AreEqual(sortedExpectedBooks[i].IsLost, sortedActualBooks[i].IsLost);
            }


        }

        protected override async Task Act()
        {
            Arrange();
            var client = TestServer.GetTestClient();
            _actual = await client.GetAsync($"/api/books/");
        }

        private void Arrange()
        {
            _expectedBooks = DataHelper.GetBooks();
        }
    }
}