using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public async Task<DailyNoteDto> CreateDailyNoteAsync(CreateDailyNoteDto request, Guid userId, Guid bookId)
        {
            ValidateIds(userId, bookId);

            if (request == null)
                throw new ArgumentNullException(nameof(request), "CreateDailyNoteDto request cannot be null.");

            var user = await _userService.GetAsync(userId) ?? throw new KeyNotFoundException($"User with ID '{userId}' was not found.");
            var book = await _bookService.GetBookByIdAsync(bookId, userId) ?? throw new KeyNotFoundException($"Book with ID '{bookId}' not found for user '{userId}'.");

            var dailyNoteEntity = _mapper.Map<DailyNote>(request);
            dailyNoteEntity.UserId = userId;
            dailyNoteEntity.BookId = bookId;
            dailyNoteEntity.CreatedAt = DateTime.UtcNow;
            dailyNoteEntity.UpdatedAt = DateTime.UtcNow;

            await _context.DailyNotes.AddAsync(dailyNoteEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DailyNoteDto>(dailyNoteEntity);
        }

        public async Task<DailyNoteDto> DeleteDailyNote(Guid userId, Guid bookId, Guid dailyNoteId)
        {
            ValidateIds(userId, bookId, dailyNoteId);

            var user = await _userService.GetAsync(userId) ?? throw new KeyNotFoundException($"User with ID '{userId}' not found.");
            var book = await _bookService.GetBookByIdAsync(bookId, userId) ?? throw new KeyNotFoundException($"Book with ID '{bookId}' not found for user '{userId}'.");
            var dailyNote = await _context.DailyNotes.FindAsync(dailyNoteId) ?? throw new KeyNotFoundException($"Daily note with ID '{dailyNoteId}' not found.");

            _context.DailyNotes.Remove(dailyNote);
            await _context.SaveChangesAsync();

            return _mapper.Map<DailyNoteDto>(dailyNote);
        }

        public async Task<IEnumerable<DailyNoteDto>> GetAllDailyNotes(Guid userId, Guid bookId)
        {
            ValidateIds(userId, bookId);

            var user = await _userService.GetAsync(userId) ?? throw new KeyNotFoundException($"User with ID '{userId}' not found.");
            var book = await _bookService.GetBookByIdAsync(bookId, userId) ?? throw new KeyNotFoundException($"Book with ID '{bookId}' not found for user '{userId}'.");

            var dailyNotes = await _context.DailyNotes
                .Where(dn => dn.UserId == userId && dn.BookId == bookId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DailyNoteDto>>(dailyNotes);
        }

        public async Task<DailyNoteDto> GetDailyNoteById(Guid userId, Guid bookId, Guid dailyNoteId)
        {
            ValidateIds(userId, bookId, dailyNoteId);

            var dailyNote = await _context.DailyNotes
                .FirstOrDefaultAsync(dn => dn.UserId == userId && dn.BookId == bookId && dn.Id == dailyNoteId)
                ?? throw new KeyNotFoundException($"Daily note with ID '{dailyNoteId}' not found.");

            return _mapper.Map<DailyNoteDto>(dailyNote);
        }

        public async Task<DailyNoteDto> UpdateDailyNoteAsync(UpdateDailyNoteDto request, Guid userId, Guid bookId, Guid dailyNoteId)
        {
            ValidateIds(userId, bookId, dailyNoteId);

            if (request == null)
                throw new ArgumentNullException(nameof(request), "UpdateDailyNoteDto request cannot be null.");

            var dailyNote = await _context.DailyNotes
                .FirstOrDefaultAsync(dn => dn.UserId == userId && dn.BookId == bookId && dn.Id == dailyNoteId)
                ?? throw new KeyNotFoundException($"Daily note with ID '{dailyNoteId}' not found.");

            dailyNote.Content = request.Content;
            dailyNote.UpdatedAt = DateTime.UtcNow;

            _context.DailyNotes.Update(dailyNote);
            await _context.SaveChangesAsync();

            return _mapper.Map<DailyNoteDto>(dailyNote);
        }

        private void ValidateIds(params Guid[] ids)
        {
            foreach (var id in ids)
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("One or more GUIDs are invalid.");
            }
        }
    }
}
