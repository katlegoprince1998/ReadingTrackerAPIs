using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Dtos
{
    public class DailyNoteDto
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required User Book { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
