using Email_Planner.Server.Data;
using Email_Planner.Shared;
using Microsoft.EntityFrameworkCore;

namespace Email_Planner.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<User>>> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return await GetUsers();
        }

        public async Task<ServiceResponse<List<User>>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUser == null)
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            else
            {

                dbUser.Deleted = true;
                await _context.SaveChangesAsync();
                return await GetUsers();
            }
        }

        public async Task<ServiceResponse<User>> GetUserById(int id)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (dbUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            else
            {
                return new ServiceResponse<User>
                {
                    Data = dbUser,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var result = await _context.Users
                .ToListAsync();
            return new ServiceResponse<List<User>>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<ServiceResponse<List<User>>> UpdateUser(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser == null)
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            else
            {
                dbUser.Email = user.Email;
                dbUser.Name = user.Name;
                dbUser.PasswordHash = user.PasswordHash;
                dbUser.PasswordSalt = user.PasswordSalt;
                dbUser.DateCreated = user.DateCreated;
                dbUser.DateUpdated = user.DateUpdated;
                dbUser.Deleted = user.Deleted;
                dbUser.Visible = user.Visible;
                await _context.SaveChangesAsync();
                return await GetUsers();
            }
        }

    }
}
