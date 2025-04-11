using AutoMapper;
using ReadingTrackerAPIs.Data;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Models.Entity;
using ReadingTrackerAPIs.Services.Books;
using ReadingTrackerAPIs.Services.Users;

namespace ReadingTrackerAPIs.Services.DailyNotes
{
    public class DailyNoteService : IDailyNoteService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;

        public DailyNoteService(ApplicationDbContext context, IMapper mapper, IUserService userService, IBookService bookService)
        {
            this._context = context;
            this._mapper = mapper;
            this._userService = userService;
            this._bookService = bookService;
            
        }
        public async Task<DailyNoteDto> CreateDailyNoteAsync(CreateDailyNoteDto request, Guid userId, Guid bookId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            if (bookId == Guid.Empty)
                throw new ArgumentException("Book ID cannot be empty.", nameof(bookId));

            if (request == null)
                throw new ArgumentNullException(nameof(request), "CreateDailyNoteDto request cannot be null.");

            var user = await _userService.GetAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

            var book = await _bookService.GetBookByIdAsync(bookId, userId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID '{bookId}' for user '{userId}' was not found.");

            var dailyNoteEntity = _mapper.Map<DailyNote>(request);
            dailyNoteEntity.UserId = userId;
            dailyNoteEntity.BookId = bookId;
            dailyNoteEntity.User = _mapper.Map<User>(user);
            dailyNoteEntity.Book = _mapper.Map<Book>(book);
            dailyNoteEntity.CreatedAt = DateTime.UtcNow;
            dailyNoteEntity.UpdatedAt = DateTime.UtcNow;

            await _context.DailyNotes.AddAsync(dailyNoteEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DailyNoteDto>(dailyNoteEntity);
        }


        async Task<DailyNoteDto> IDailyNoteService.DeleteDailyNote(Guid userId, Guid bookId, Guid dailyNoteId)
        {
            if(bookId ==  Guid.Empty || userId == Guid.Empty) throw new ArgumentNullException(nameof(userId),nameof(userId)
                + " UserId or BookId cannot be null");

            var user = _userService.GetAsync(userId);

            if (user == null)
                throw new ArgumentNullException(nameof(user), $" user with ID {userId} was not found");

            var book = _bookService.GetBookByIdAsync(bookId, userId);

            if (book == null)
                throw new ArgumentNullException(nameof(book), $" book was not found wit ID {bookId}");

            var dailyNote = await _context.DailyNotes.FindAsync(dailyNoteId);

            if (dailyNote == null) throw new ArgumentNullException($"Daily note was not found with ID {dailyNote}");

            _context.DailyNotes.Remove(dailyNote);
            await _context.SaveChangesAsync();

            var removedDailyNote = _mapper.Map<DailyNote>(dailyNote);
            removedDailyNote.User = _mapper.Map<User>(user);
            removedDailyNote.Book = _mapper.Map<Book>(book);
            removedDailyNote.UserId = userId;
            removedDailyNote.BookId = bookId;

            return _mapper.Map<DailyNoteDto>(removedDailyNote);

        }

        Task<DailyNoteDto> IDailyNoteService.GetAllDailyNotes(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task<DailyNoteDto> IDailyNoteService.GetDailyNoteById(Guid userId, Guid dailyNoteId)
        {
            throw new NotImplementedException();
        }

        Task<DailyNoteDto> IDailyNoteService.UpdateDailyNoteAsync(UpdateDailyNoteDto request, Guid userId, Guid bookId, Guid dailyNoteId)
        {
            throw new NotImplementedException();
        }
    }
}
