using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Mappers;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _dbContext.Books
                .Include(x => x.Reader)
                .AsNoTracking()
                .ToListAsync();

            return Ok(books.Select(x => x.Map()));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            var bookExist = await _dbContext.Books.FirstOrDefaultAsync(x => x.ISBN == book.ISBN);
            if (bookExist != null)
            {
                return BadRequest("Book already exist, ISBN should be unique");
            }

            await _dbContext.Books.AddAsync(book.Map());
            await _dbContext.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Book book)
        {
            var bookEntity = await _dbContext.Books.FirstOrDefaultAsync(x => x.ISBN == book.ISBN);

            UpdateProperties(bookEntity, book);

            var entityEntry = _dbContext.Books.Update(bookEntity);
            await _dbContext.SaveChangesAsync();

            return Ok(entityEntry.Entity.Map());
        }

        private void UpdateProperties(BookEntity bookExist, Book book)
        {
            bookExist.Checkout = book.Checkout;
            bookExist.Title = book.Title;
            bookExist.Title = book.Title;
            bookExist.Reader = book.Reader.Map();
        }
    }
}