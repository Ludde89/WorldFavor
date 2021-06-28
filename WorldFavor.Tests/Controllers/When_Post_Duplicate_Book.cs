using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Post_Duplicate_Book : TestBase
    {
        private HttpResponseMessage _actual;
        private BookEntity _expectedBook;

        [TestMethod]
        public async Task Then_Expected_Books_Should_Be_Created()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.BadRequest);
            var message = await _actual.Content.ReadAsStringAsync();
            Assert.AreEqual(message, "Book already exist, ISBN should be unique");
        }

        protected override async Task Act()
        {
            Arrange();
            var client = TestServer.GetTestClient();
            var x = new Book
            {
                ISBN = _expectedBook.ISBN
            };
            var searlizedBook = JsonConvert.SerializeObject(_expectedBook);
            _actual = await client.PostAsync($"/api/books/", new StringContent(searlizedBook, Encoding.UTF8, "application/json"));
        }

        private void Arrange()
        {
            var books = DataHelper.GetBooks().ToList();
            _expectedBook = books[3];
        }
    }
}