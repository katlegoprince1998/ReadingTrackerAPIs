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
            this._context = context;
            this._mapper = mapper;
        }

         async Task<UserDto> IUserService.CreateAsync(UserDto user)
        {
           if(user == null)
                throw new ArgumentNullException("Object cannot be null {}" + user);

           var mappedUser = _mapper.Map<User>(user);

            await _context.Users.AddAsync(mappedUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(mappedUser);
        }

         async Task<UserDto> IUserService.DeleteAsync(Guid id)
        {
           if(id == Guid.Empty)
                throw new ArgumentNullException("Id is empty or null, Please provide user id");

            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User was not found");

             _context.Users.Remove(user);
            await _context.Users.FindAsync();

            return _mapper.Map<UserDto>(user);
        }

         async Task<IEnumerable<UserDto>> IUserService.GetAllUsersAsync()
        {
           var users = await _context.Users.ToListAsync();

            if (users == null || users.Count == 0)
                return Enumerable.Empty<UserDto>();

            return _mapper.Map<List<UserDto>>(users);
        }

         async Task<UserDto> IUserService.GetAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id cannot be null or empty");

            var user =  await _context.Users.FindAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User was not found");

            return _mapper.Map<UserDto>(user);
        }

        async Task<UserDto> IUserService.UpdateAsync(UserUpdateDto user, Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("User ID cannot be null");

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {id} was not found");

            existingUser.ProfilePicture = user.ProfilePicture == null ? existingUser.ProfilePicture : user.ProfilePicture;
            existingUser.Fullname = user.Fullname == null ? existingUser.Fullname : user.Fullname;

             _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(existingUser);


        }
    }
}
