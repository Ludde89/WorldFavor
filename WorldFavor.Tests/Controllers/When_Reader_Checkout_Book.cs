using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using WorldFavor.Contracts.Entities;
using WorldFavor.Persistence.DbContext;
using WorldFavor.Tests.MockData;

namespace WorldFavor.Tests.Controllers
{
    [TestClass]
    public class When_Reader_Checkout_Book : TestBase
    {
        private HttpResponseMessage _actual;
        private ReaderEntity _expectedReader;
        private BookEntity _expectedBook;

        [TestMethod]
        public async Task Then_Expected_Book_Should_Be_CheckedOut()
        {
            Assert.AreEqual(HttpStatusCode.OK, _actual.StatusCode);

            await using var context = new WorldFavorDbContext(ContextOptionsBuilder.Options);

            var actualReader = await context
                .Readers
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Name == _expectedReader.Name && x.Birth == _expectedReader.Birth);

            Assert.AreEqual(_expectedReader.Name, actualReader.Name);
            Assert.AreEqual(_expectedReader.Birth, actualReader.Birth);
            Assert.AreEqual(_expectedBook.ISBN, actualReader.Books.First().ISBN);
            Assert.IsNotNull(actualReader.Books.First().Checkout);
            Assert.AreEqual(_expectedBook.ISBN, actualReader.Books.First().ISBN);
            Assert.AreEqual(_expectedBook.Title, actualReader.Books.First().Title);
        }

        protected override async Task Act()
        {
            var client = TestServer.GetTestClient();

            var searlizedReader = JsonConvert.SerializeObject(_expectedReader);
            _actual = await client.PutAsync($"/api/readers/{_expectedReader.Name}/{_expectedBook.ISBN}", new StringContent(searlizedReader, Encoding.UTF8, "application/json"));
        }

        protected override void Arrange()
        {
            _expectedReader = DataHelper.GetReaders().ToList()[2];
            _expectedBook = DataHelper.GetBooks().ToList()[2];
            
        }
    }
}