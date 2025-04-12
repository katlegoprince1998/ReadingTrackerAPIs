using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingTrackerAPIs.Models.Entity
{
    public class ReadingProgress
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }

        public required Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public required Book Book { get; set; }

        public int CurrentPage { get; set; } = 0;

        public int Totalpages { get; set; } = 0;

        public int DailyGoal { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }

}
