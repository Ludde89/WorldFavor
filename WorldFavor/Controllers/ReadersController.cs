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
            var reader = await _dbContext.Readers.FirstOrDefaultAsync(x => x.Name == name);

            var result = reader.Map();

            return Ok(result);
        }
    }
}
