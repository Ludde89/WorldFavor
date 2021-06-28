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
    public class When_Post_Book : TestBase
    {
        private HttpResponseMessage _actual;
        private Book _expectedBook;

        [TestMethod]
        public async Task Then_Expected_Books_Should_Be_Created()
        {
            Assert.AreEqual(_actual.StatusCode, HttpStatusCode.Created);

            await using var context = new WorldFavorDbContext(ContextOptionsBuilder.Options);

            var actualBook = await context.Books.FirstOrDefaultAsync(x => x.ISBN == _expectedBook.ISBN);

            Assert.AreEqual(_expectedBook.Title, actualBook.Title);
            Assert.AreEqual(_expectedBook.ISBN, actualBook.ISBN);
            Assert.AreEqual(_expectedBook.IsLost, actualBook.IsLost);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();
            _expectedBook = new Book
            {
                ISBN = "foo",
                IsLost = false,
                Title = "foobar"
            };
            var searlizedBook = JsonConvert.SerializeObject(_expectedBook);
            _actual = await client.PostAsync($"/api/books/", new StringContent(searlizedBook, Encoding.UTF8, "application/json"));
        }

    }
}