namespace ReadingTrackerAPIs.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
