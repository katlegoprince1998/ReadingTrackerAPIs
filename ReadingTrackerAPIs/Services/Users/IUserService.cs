using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;

namespace ReadingTrackerAPIs.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto user);
        Task<UserDto> UpdateAsync(UserUpdateDto user, Guid id);
        Task<UserDto> DeleteAsync(Guid id);
        Task<UserDto> GetAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
