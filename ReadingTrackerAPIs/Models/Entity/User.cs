using System.ComponentModel.DataAnnotations;

namespace ReadingTrackerAPIs.Models.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public required string Fullname { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }

        public string? ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required ICollection<Book> Books { get; set; }
        public required ICollection<ReadingProgress> ReadingProgresses { get; set; }
        public required ICollection<YearlyGoal> YearlyGoals { get; set; }
        public required ICollection<DailyNote> DailyNotes { get; set; }
    }
}
