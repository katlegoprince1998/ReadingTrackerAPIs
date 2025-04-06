using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReadingTrackerAPIs.Models.MyEnums;

namespace ReadingTrackerAPIs.Models.Entity
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public int TotalPages { get; set; }

        public string? CoverImage { get; set; }

        [EnumDataType(typeof(BookStatus))]
        public BookStatus Status { get; set; } = BookStatus.NotStarted;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required ICollection<ReadingProgress> ReadingProgresses { get; set; }
        public required ICollection<DailyNote> DailyNotes { get; set; }
    }
}
