﻿using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;

namespace ReadingTrackerAPIs.Services.DailyNotes
{
    public interface IDailyNoteService
    {
        Task<DailyNoteDto> CreateDailyNoteAsync(CreateDailyNoteDto request, Guid userId, Guid bookId);
        Task<DailyNoteDto> UpdateDailyNoteAsync(UpdateDailyNoteDto request, Guid userId, Guid bookId);
        Task<DailyNoteDto> DeleteDailyNote(Guid userId, Guid bookId);
        Task<DailyNoteDto> GetAllDailyNotes(Guid userId);
        Task<DailyNoteDto> GetDailyNoteById(Guid userId, Guid dailyNoteId);
    }
}
