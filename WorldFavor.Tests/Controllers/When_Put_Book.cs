using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Put_Book : TestBase
    {
        private HttpResponseMessage _actual;
        private Book _expectedBook;

        [TestMethod]
        public async Task Then_Expected_Books_Should_Be_Created()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.OK);

            var book = JsonConvert.DeserializeObject<Book>(await _actual.Content.ReadAsStringAsync());

            Assert.AreEqual(_expectedBook.ISBN, book.ISBN);
            Assert.AreEqual(_expectedBook.Checkout, book.Checkout);
            Assert.AreEqual(_expectedBook.Title, book.Title);
            Assert.AreEqual(_expectedBook.Reader, book.Reader);
            Assert.AreEqual(_expectedBook.IsLost, book.IsLost);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            var searlizedBook = JsonConvert.SerializeObject(_expectedBook);
            _actual = await client.PutAsync($"/api/books/", new StringContent(searlizedBook, Encoding.UTF8, "application/json"));
        }

        protected override void Arrange()
        {
            var bookEntity = DataHelper.GetBooks().ToList()[4];
            _expectedBook = new Book
            {
                ISBN = bookEntity.ISBN,
                IsLost = false,
                Title = "newTitle"
            };
            base.Arrange();
        }
    }
}