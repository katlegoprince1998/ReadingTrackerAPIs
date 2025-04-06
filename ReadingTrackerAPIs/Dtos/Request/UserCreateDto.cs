namespace ReadingTrackerAPIs.Dtos.Request
{
    public class UserCreateDto
    {
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
