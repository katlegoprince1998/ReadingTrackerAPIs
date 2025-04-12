using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;

namespace ReadingTrackerAPIs.Services.YearlyGoals
{
    public interface IYearlyGoalService
    {
        Task<YearlyGoalDto> createYearlyGoal(CreateYearlyGoalDto request, Guid userId);
        Task<YearlyGoalDto> updateYearlyGoal(UpdateYearlyGoalDto request, Guid userId, Guid id);
        Task<YearlyGoalDto> deleteYearlyGoal(Guid id);
        Task<IEnumerable<YearlyGoalDto>> getAllYearlyGoal(Guid userId);
        Task<YearlyGoalDto> GetYearlyGoalById(Guid id);

   
    }
}
