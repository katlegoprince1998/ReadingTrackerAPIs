using ReadingTrackerAPIs.Dtos;

namespace ReadingTrackerAPIs.Services.Books
{
    public interface IBookService
    {
        Task<BookDto> CreateBookAsync(BookDto book);
        Task<BookDto> UpdateBookAsync(BookDto book, Guid id);
        Task<BookDto> DeleteBookAsync(Guid id);
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<IEnumerable<BookDto>> GetAllBooksAsync();

    }
}
