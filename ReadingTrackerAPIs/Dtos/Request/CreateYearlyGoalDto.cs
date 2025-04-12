using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Dtos.Request
{
    public class CreateYearlyGoalDto
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int GoalCount { get; set; }
        public int BooksRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
