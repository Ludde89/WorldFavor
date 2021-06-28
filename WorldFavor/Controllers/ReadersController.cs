using System;
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
    public class ReadersController : ControllerBase
    {
        private readonly WorldFavorDbContext _dbContext;

        public ReadersController(WorldFavorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<Reader>> Get(string name)
        {
            var reader = await _dbContext
                .Readers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            var result = reader.Map();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetAll()
        {
            var readers = await _dbContext.Readers.AsNoTracking().ToListAsync();

            var result = readers.Select(x => x.Map());

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Reader reader)
        {
            var readerExist = await _dbContext
                .Readers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == reader.Name && x.Birth == reader.Birth);

            if (readerExist != null)
            {
                return BadRequest("Reader already exist");
            }

            await _dbContext.Readers.AddAsync(reader.Map());

            await _dbContext.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut]
        [Route("{name}/{ISBN}")]
        public async Task<ActionResult<Reader>> CheckoutBook(string name, string isbn)
        {
            var reader = await _dbContext.Readers.Include(x => x.Books).FirstOrDefaultAsync(x => x.Name == name);
            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.ISBN == isbn);
            book.Checkout = DateTime.UtcNow;

            reader.Books = reader.Books.Concat(new[] {book}).ToList();

            var entityEntry = _dbContext.Readers.Update(reader);

            await _dbContext.SaveChangesAsync();

            return Ok(entityEntry.Entity.Map());
        }
    }
}
