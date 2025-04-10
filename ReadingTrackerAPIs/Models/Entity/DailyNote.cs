using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingTrackerAPIs.Models.Entity
{
    public class DailyNote
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        public Guid? BookId { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
