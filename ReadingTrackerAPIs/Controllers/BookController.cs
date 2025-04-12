using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Services.Books;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] BookDto bookDto, Guid userId)
        {
            if (bookDto == null)
                return BadRequest("Book data is required.");

            var createdBook = await _bookService.CreateBookAsync(bookDto, userId);

            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id, userId }, createdBook);
        }

        [HttpPut("{id}/user/{userId}")]
        public async Task<ActionResult<BookDto>> UpdateBook(Guid id, Guid userId, [FromBody] BookDto bookDto)
        {
            if (id == Guid.Empty)
                return BadRequest("Book ID cannot be empty.");
            if (bookDto == null)
                return BadRequest("Book data is required.");

            var updatedBook = await _bookService.UpdateBookAsync(bookDto, id, userId);

            if (updatedBook == null)
                return NotFound($"Book with ID {id} not found or access denied.");

            return Ok(updatedBook);
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<ActionResult<BookDto>> DeleteBook(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                return BadRequest("Book ID cannot be empty.");

            var deletedBook = await _bookService.DeleteBookAsync(id, userId);

            if (deletedBook == null)
                return NotFound($"Book with ID {id} not found or access denied.");

            return Ok(deletedBook);
        }

        [HttpGet("{id}/user/{userId}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                return BadRequest("Book ID cannot be empty.");

            var book = await _bookService.GetBookByIdAsync(id, userId);

            if (book == null)
                return NotFound($"Book with ID {id} not found or access denied.");

            return Ok(book);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks(Guid userId)
        {
            var books = await _bookService.GetAllBooksAsync(userId);
            return Ok(books);
        }
    }
}
