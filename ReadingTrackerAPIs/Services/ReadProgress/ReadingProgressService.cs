using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingTrackerAPIs.Data;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Models.Entity;
using ReadingTrackerAPIs.Services.Books;
using ReadingTrackerAPIs.Services.Users;

namespace ReadingTrackerAPIs.Services.ReadProgress
{
    public class ReadingProgressService : IReadingProgressService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;

        public ReadingProgressService(ApplicationDbContext context, IMapper mapper, IUserService userService, IBookService bookService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }
        public async Task<ReadingProgressDto> CreateReadingProgress(CreateReadingProgressDto Dto, Guid UserId, Guid BookId)
        {
            ValidateIds(UserId, BookId);

            var user = await _userService.GetAsync(UserId) ?? throw new KeyNotFoundException($"User with ID '{UserId}' was not found.");
            var book = await _bookService.GetBookByIdAsync(BookId, UserId) ?? throw new KeyNotFoundException($"Book with ID '{BookId}' not found for user '{UserId}'.");

            if (Dto == null) throw new ArgumentNullException(nameof(Dto), "Object cannot be null");

            var readingProgressEntinty = _mapper.Map<ReadingProgress>(Dto);
            readingProgressEntinty.User = _mapper.Map<User>(user);
            readingProgressEntinty.Book = _mapper.Map<Book>(book);

            await _context.ReadingProgress.AddAsync(readingProgressEntinty);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadingProgressDto>(readingProgressEntinty);

           
        }

        public async Task<ReadingProgressDto> DeleteReadingProgress(Guid UserId, Guid BookId, Guid ReadingProgressId)
        {
           ValidateIds(UserId, BookId, ReadingProgressId);


            var user = await _userService.GetAsync(UserId) ?? 
                throw new KeyNotFoundException($"User with ID '{UserId}' was not found.");

            var book = await _bookService.GetBookByIdAsync(BookId, UserId)
                ?? throw new KeyNotFoundException($"Book with ID '{BookId}' not found for user '{UserId}'.");

            var readingProgress  = await _context.ReadingProgress.FindAsync(ReadingProgressId) 
                ?? throw new KeyNotFoundException($"Reading progress with ID {ReadingProgressId}, was not found");


            var removedreadingProgress =  _context.ReadingProgress.Remove(readingProgress) ?? throw new Exception("Failed to remove reading progress");


            return _mapper.Map<ReadingProgressDto>(removedreadingProgress);


        }

        public async Task<IEnumerable<ReadingProgressDto>> GetReadingProgress(Guid userId, Guid bookId)
        {
            ValidateIds(userId, bookId);

            var user = await _userService.GetAsync(userId)
                ?? throw new KeyNotFoundException($"User with ID '{userId}' was not found.");

            var book = await _bookService.GetBookByIdAsync(bookId, userId)
                ?? throw new KeyNotFoundException($"Book with ID '{bookId}' not found for user '{userId}'.");

            var readingProgressList = await _context.ReadingProgress
                .Where(rp => rp.UserId == userId && rp.BookId == bookId)
                .ToListAsync();

            return _mapper.Map<List<ReadingProgressDto>>(readingProgressList);
        }


        public async Task<ReadingProgressDto> GetReadingProgressById(Guid UserId, Guid BookId, Guid id)
        {
            ValidateIds(UserId, BookId, id);


            var user = await _userService.GetAsync(UserId) 
                ?? throw new KeyNotFoundException($"User with ID '{UserId}' was not found.");

            var book = await _bookService.GetBookByIdAsync(BookId, UserId) 
                ?? throw new KeyNotFoundException($"Book with ID '{BookId}' not found for user '{UserId}'.");

            var readingProgress = await _context.ReadingProgress.FindAsync(id)
             ?? throw new KeyNotFoundException($"Reading progress with ID {id}, was not found");

            return _mapper.Map<ReadingProgressDto>(readingProgress);


        }

        public async Task<ReadingProgressDto> UpdateReadingProgress(UpdateReadingProgressDto Dto, Guid UserId, Guid BookId, Guid ReadingProgressId)
        {
            ValidateIds(UserId, BookId, ReadingProgressId);


            var user = await _userService.GetAsync(UserId)
                ?? throw new KeyNotFoundException($"User with ID '{UserId}' was not found.");

            var book = await _bookService.GetBookByIdAsync(BookId, UserId) 
                ?? throw new KeyNotFoundException($"Book with ID '{BookId}' not found for user '{UserId}'.");

            var readingProgress = await _context.ReadingProgress.FindAsync(ReadingProgressId)
            ?? throw new KeyNotFoundException($"Reading progress with ID {ReadingProgressId}, was not found");

            readingProgress.CurrentPage = Dto.CurrentPage;
            readingProgress.LastUpdated = DateTime.Now;
            readingProgress.DailyGoal = Dto.DailyGoal;
            readingProgress.Totalpages = Dto.Totalpages;

            _context.ReadingProgress.Update(readingProgress);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadingProgressDto>(readingProgress);
        }

        private void ValidateIds(params Guid[] ids)
        {
            foreach (var id in ids)
            {
                if (id != Guid.Empty) 
                    throw new ArgumentNullException(nameof(id), "One ID is missing (UserId, BookId or ReadingId");

            }
        }
    }
}
