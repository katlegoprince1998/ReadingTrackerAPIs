using ReadingTrackerAPIs.Models.MyEnums;

namespace ReadingTrackerAPIs.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int TotalPages { get; set; }
        public string? CoverImage { get; set; }
        public BookStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
