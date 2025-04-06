using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingTrackerAPIs.Models.Entity
{
    public class YearlyGoal
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        public int Year { get; set; }

        public int GoalCount { get; set; }

        public int BooksRead { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
