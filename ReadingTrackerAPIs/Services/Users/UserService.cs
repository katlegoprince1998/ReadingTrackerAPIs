using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingTrackerAPIs.Data;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto), "User data cannot be null.");

            var user = _mapper.Map<User>(userDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(id));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{id}' was not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(); 

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(id));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{id}' was not found.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserUpdateDto updateDto, Guid id)
        {
            if (updateDto == null)
                throw new ArgumentNullException(nameof(updateDto), "Update data cannot be null.");

            if (id == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(id));

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID '{id}' was not found.");

            user.Fullname = updateDto.Fullname ?? user.Fullname;
            user.ProfilePicture = updateDto.ProfilePicture ?? user.ProfilePicture;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}
