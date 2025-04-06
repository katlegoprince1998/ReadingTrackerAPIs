namespace ReadingTrackerAPIs.Dtos
{
    public class ReadingProgressDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int CurrentPage { get; set; }
        public int DailyGoal { get; set; }
        public int StreakCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
