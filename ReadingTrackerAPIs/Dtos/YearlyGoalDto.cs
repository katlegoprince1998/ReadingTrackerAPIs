namespace ReadingTrackerAPIs.Dtos
{
    public class YearlyGoalDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int GoalCount { get; set; }
        public int BooksRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
