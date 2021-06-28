using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFavor.Contracts.Dtos;
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
    }
}