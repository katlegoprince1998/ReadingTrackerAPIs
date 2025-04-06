using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Services.Books;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Book Id cannot be empty.");

            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound("Book not found.");

            return Ok(book);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> deleteBook(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Book Id cannot be null");

            var deletedBook = _bookService.DeleteBookAsync(id);

            if (deletedBook == null)
                return BadRequest("Unable to delete a book");

            return Ok(deletedBook);

        }

        [HttpPost]
        public async Task<IActionResult> createBook(BookDto dto)
        {
            if (dto == null)
                return BadRequest("Failed to create a book, please ensure that all required information is " +
                    "provided");

            var createdBook = await _bookService.CreateBookAsync(dto);

            if (createdBook == null)
                return BadRequest("Failed to create a book, please ensure that all required information is " +
                     "provided");

            return Ok(createdBook);
        }


        [HttpPut("id")]
        public async Task<IActionResult> updateBook(Guid id, BookDto dto)
        {
            if (dto == null)
                return BadRequest("Failed to create a book, please ensure that all required information is " +
                    "provided");

            if(dto == null)
                return BadRequest("Failed to create a book, please ensure that all required information is " +
                    "provided");

            var updatedBook = await _bookService.UpdateBookAsync(dto, id);

            if (updatedBook == null)
                return BadRequest("Failed to update book Id");

            return Ok(updatedBook);
        }
        

    }



}
