using ReadingTrackerAPIs.Dtos;
using AutoMapper;
using ReadingTrackerAPIs.Data;
using Microsoft.EntityFrameworkCore;
using ReadingTrackerAPIs.Models.Entity;
using ReadingTrackerAPIs.Models.MyEnums;

namespace ReadingTrackerAPIs.Services.Books
{ 
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public BookService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto, Guid userId)
        {
            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto), "Book object cannot be null.");

            var bookEntity = _mapper.Map<Book>(bookDto);
            bookEntity.UserId = userId;
            bookEntity.CreatedAt = DateTime.UtcNow;

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(bookEntity);
        }

        public async Task<BookDto> DeleteBookAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Book ID cannot be empty.");

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found or access denied.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(Guid userId)
        {
            var books = await _context.Books
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Book ID cannot be empty.");

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found or access denied.");

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(BookDto bookDto, Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Book ID cannot be empty.");
            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto), "Book data cannot be null.");

            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (existingBook == null)
                throw new KeyNotFoundException($"Book with ID {id} not found or access denied.");

            existingBook.Title = !string.IsNullOrWhiteSpace(bookDto.Title) ? bookDto.Title : existingBook.Title;
            existingBook.Author = !string.IsNullOrWhiteSpace(bookDto.Author) ? bookDto.Author : existingBook.Author;
            existingBook.TotalPages = bookDto.TotalPages > 0 ? bookDto.TotalPages : existingBook.TotalPages;
            existingBook.CoverImage = bookDto.CoverImage ?? existingBook.CoverImage;

            if (!Enum.IsDefined(typeof(BookStatus), bookDto.Status))
                throw new ArgumentException("Invalid book status provided.");

            existingBook.Status = bookDto.Status;
            existingBook.UpdatedAt = DateTime.UtcNow;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(existingBook);
        }
    }
}
