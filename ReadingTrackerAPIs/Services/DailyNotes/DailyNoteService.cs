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
        async Task<DailyNoteDto> IDailyNoteService.CreateDailyNoteAsync(CreateDailyNoteDto request, Guid userId, Guid bookId)
        {
           if (userId == Guid.Empty || bookId == Guid.Empty) 
                throw new ArgumentNullException("User Id or Book Id cannot be empty");

           if (request == null)
                throw new ArgumentNullException(nameof(request), " cannot be null");

           var user = _userService.GetAsync(userId);

           if (user == null)
                throw new ArgumentNullException(nameof(user), $" user with ID {userId} was not found");

           var book = _bookService.GetBookByIdAsync(bookId, userId);

            if (book == null) 
                throw new ArgumentNullException(nameof(book) ,$" book was not found wit ID {bookId}");


            var dailyNoteEntity = _mapper.Map<DailyNote>(request);
            dailyNoteEntity.UserId = userId;
            dailyNoteEntity.BookId = bookId;
            dailyNoteEntity.User = _mapper.Map<User>(user);
            dailyNoteEntity.Book = _mapper.Map<Book>(book);
            dailyNoteEntity.CreatedAt = DateTime.UtcNow;
            dailyNoteEntity.UpdatedAt = DateTime.UtcNow;
            -
            await _context.DailyNotes.AddAsync(dailyNoteEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DailyNoteDto>(dailyNoteEntity);



        }

        Task<DailyNoteDto> IDailyNoteService.DeleteDailyNote(Guid userId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        Task<DailyNoteDto> IDailyNoteService.GetAllDailyNotes(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task<DailyNoteDto> IDailyNoteService.GetDailyNoteById(Guid userId, Guid dailyNoteId)
        {
            throw new NotImplementedException();
        }

        Task<DailyNoteDto> IDailyNoteService.UpdateDailyNoteAsync(UpdateDailyNoteDto request, Guid userId, Guid bookId)
        {
            throw new NotImplementedException();
        }
    }
}
