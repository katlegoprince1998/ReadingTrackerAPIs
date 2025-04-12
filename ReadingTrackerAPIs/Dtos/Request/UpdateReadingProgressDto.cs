namespace ReadingTrackerAPIs.Dtos.Request
{
    public class UpdateReadingProgressDto
    {
        public int CurrentPage { get; set; }
        public int Totalpages { get; set; }
        public int DailyGoal { get; set; }
    }
}
