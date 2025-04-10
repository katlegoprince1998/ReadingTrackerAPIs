

namespace ReadingTrackerAPIs.Dtos.Request
{
    public class CreateDailyNoteDto
    {
        public required Guid UserId { get; set; }
        public Guid? BookId { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
