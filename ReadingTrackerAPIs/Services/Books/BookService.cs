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
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BookDto> CreateBookAsync(BookDto bookDto, Guid userId)
        {
            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto), "Book object cannot be null.");

            var book = _mapper.Map<Book>(bookDto);
            book.UserId = userId;
            book.CreatedAt = DateTime.UtcNow;
            book.Status = Enum.IsDefined(typeof(BookStatus), bookDto.Status) ? bookDto.Status : BookStatus.Unread;

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> DeleteBookAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Book ID cannot be empty.", nameof(id));

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
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
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Book ID cannot be empty.", nameof(id));

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found or access denied.");

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(BookDto bookDto, Guid id, Guid userId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Book ID cannot be empty.", nameof(id));
            if (bookDto == null)
                throw new ArgumentNullException(nameof(bookDto), "Book data cannot be null.");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found or access denied.");

          
            book.Title = !string.IsNullOrWhiteSpace(bookDto.Title) ? bookDto.Title : book.Title;
            book.Author = !string.IsNullOrWhiteSpace(bookDto.Author) ? bookDto.Author : book.Author;
            book.TotalPages = bookDto.TotalPages > 0 ? bookDto.TotalPages : book.TotalPages;
            book.CoverImage = bookDto.CoverImage ?? book.CoverImage;

            if (!Enum.IsDefined(typeof(BookStatus), bookDto.Status))
                throw new ArgumentException("Invalid book status provided.");
            book.Status = bookDto.Status;

            book.UpdatedAt = DateTime.UtcNow;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }
    }
}
