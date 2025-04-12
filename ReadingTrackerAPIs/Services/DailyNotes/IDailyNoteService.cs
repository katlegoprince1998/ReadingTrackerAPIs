using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;

namespace ReadingTrackerAPIs.Services.DailyNotes
{
    public interface IDailyNoteService
    {
        Task<DailyNoteDto> CreateDailyNoteAsync(CreateDailyNoteDto request, Guid userId, Guid bookId);
        Task<DailyNoteDto> UpdateDailyNoteAsync(UpdateDailyNoteDto request, Guid userId, Guid bookId, Guid dailyNoteId);
        Task<DailyNoteDto> DeleteDailyNote(Guid userId, Guid bookId, Guid dailyNoteId);
        Task<IEnumerable<DailyNoteDto>> GetAllDailyNotes(Guid userId, Guid bookId);
        Task<DailyNoteDto> GetDailyNoteById(Guid userId, Guid dailyNoteId, Guid bookId);
    }
}
