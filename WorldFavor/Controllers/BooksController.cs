using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorldFavor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [Route("{title}")]
        public async Task<ActionResult<Book>> Get(string title)
        {
            return Ok( new Book(title));
        }
    }

    public class Book
    {
        public string Title { get; }

        public Book(string title)
        {
            Title = title;
        }
    }
}