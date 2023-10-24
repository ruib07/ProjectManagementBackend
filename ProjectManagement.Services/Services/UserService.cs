using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface IUsersService
    {
        Task<List<UserEfo>> GetAllUsersAsync();
        Task<UserEfo> GetUserByIdAsync(int userId);
        Task<UserEfo> GetUserByNameAsync(string username);
        Task<UserEfo> UpdateUserProfileAsync(string username, UserEfo updateUser);
        Task DeleteUserAsync(int userId);
    }

    public class UserService : IUsersService
    {
        private readonly PManagementDbContext _context;

        public UserService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEfo>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEfo> GetUserByIdAsync(int userId)
        {
            UserEfo? user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return user;
        }

        public async Task<UserEfo> GetUserByNameAsync(string username)
        {
            UserEfo? user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return user;
        }

        public async Task<UserEfo> UpdateUserProfileAsync(string username, UserEfo updateUser)
        {
            try
            {
                UserEfo? user = await _context.Users.FirstOrDefaultAsync(
                    u => u.UserName == username);

                if (user == null)
                {
                    throw new Exception("Entity doesn´t exist in the database");
                }

                user.UserName = updateUser.UserName;
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.Function = updateUser.Function;
                user.ImageURL = updateUser.ImageURL;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            UserEfo? user = await _context.Users.FirstOrDefaultAsync(
                u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
