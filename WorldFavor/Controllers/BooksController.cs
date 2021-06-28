using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Persistence.DbContext;

namespace WorldFavor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly WorldFavorDbContext _dbContext;

        public BooksController(WorldFavorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{title}")]
        public async Task<ActionResult<Book>> Get(string title)
        {
            var book = await _dbContext.Books
                .Include(x => x.Reader)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);

            return Ok(book.Map());
        }
    }

    public static class BookMapper
    {
        public static Book Map(this BookEntity entity)
        {
            return new Book
            {
                Checkout = entity.Checkout,
                ISBN = entity.ISBN,
                IsLost = entity.IsLost,
                Title = entity.Title
            };
        }
    }

    public static class ReaderMapper
    {
        public static Reader Map(this ReaderEntity entity)
        {
            return new Reader
            {
                Name = entity.Name,
                Birth = entity.Birth
            };
        }
    }
}