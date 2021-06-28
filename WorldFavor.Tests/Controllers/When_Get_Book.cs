using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Controllers;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Get_Book : TestBase
    {
        private HttpResponseMessage _actual;
        private BookEntity _expectedBook;

        [TestMethod]
        public async Task Then_Expected_Book_Should_Be_Returned()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.OK);
            var book = JsonConvert.DeserializeObject<Book>(await _actual.Content.ReadAsStringAsync());

            Assert.AreEqual(_expectedBook.Title, book.Title);
            Assert.AreEqual(_expectedBook.Checkout, book.Checkout);
            Assert.AreEqual(_expectedBook.ISBN, book.ISBN);
            Assert.AreEqual(_expectedBook.IsLost, book.IsLost);
        }

        protected override async Task Act()
        {
            Arrange();
            var client = TestServer.GetTestClient();
            _actual = await client.GetAsync($"/api/books/{_expectedBook.Title}");
        }

        private void Arrange()
        {
            var bookEntities = DataHelper.GetBooks();
            _expectedBook = bookEntities.ToList()[2];
        }
    }
}
