using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;

namespace ReadingTrackerAPIs.Services.ReadProgress
{
    public interface IReadingProgressService
    {
        Task<ReadingProgressDto> CreateReadingProgress(CreateReadingProgressDto Dto, Guid UserId, Guid BookId);
        Task<ReadingProgressDto> UpdateReadingProgress(UpdateReadingProgressDto Dto, Guid UserId, Guid BookId, Guid ReadingProgressId);
        Task<ReadingProgressDto> DeleteReadingProgress(Guid UserId, Guid BookId, Guid ReadingProgressId);
        Task<IEnumerable<ReadingProgressDto>> GetReadingProgress(Guid UserId, Guid BookId);
        Task<ReadingProgressDto> GetReadingProgressById(Guid UserId, Guid BookId, Guid id);

    }
}
