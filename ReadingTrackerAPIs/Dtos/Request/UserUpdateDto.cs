namespace ReadingTrackerAPIs.Dtos.Request
{
    public class UserUpdateDto
    {
        public required string Fullname { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
