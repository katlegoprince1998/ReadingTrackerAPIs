using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Dtos
{
    public class ReadingProgressDto
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required Book Book { get; set; }
        public int CurrentPage { get; set; }
        public int Totalpages { get; set; }
        public int DailyGoal { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}
