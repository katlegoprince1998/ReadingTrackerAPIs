namespace ReadingTrackerAPIs.Dtos.Request
{
    public class CreateReadingProgressDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int CurrentPage { get; set; }
        public int Totalpages { get; set; }
        public int DailyGoal { get; set; }
    }
}
