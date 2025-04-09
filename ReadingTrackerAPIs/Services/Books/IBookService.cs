using ReadingTrackerAPIs.Dtos;

namespace ReadingTrackerAPIs.Services.Books
{
    public interface IBookService
    {
        Task<BookDto> CreateBookAsync(BookDto book, Guid userId);
        Task<BookDto> UpdateBookAsync(BookDto book, Guid id, Guid userId);
        Task<BookDto> DeleteBookAsync(Guid id, Guid userId);
        Task<BookDto> GetBookByIdAsync(Guid id, Guid userId);
        Task<IEnumerable<BookDto>> GetAllBooksAsync(Guid userId);
    }
}
