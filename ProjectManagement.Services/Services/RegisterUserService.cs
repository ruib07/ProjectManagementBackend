using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities.Efos;
using ProjectManagement.EntityFramework;

namespace ProjectManagement.Services.Services
{
    public interface IRegisterUserService
    {
        Task<List<RegisterUserEfo>> GetAllRegisterUsersAsync();
        Task<RegisterUserEfo> GetRegisterUserByIdAsync(int registerUserId);
        Task<RegisterUserEfo> SendRegisterUserAsync(RegisterUserEfo registerUser);
        Task<RegisterUserEfo> SendLoginUserAsync(string username, string password);
        Task<UserEfo> SendNewUserProfileAsync(string username, string password, int registoUserId);
        Task DeleteRegisterUserAsync(int registerUserId);
    }

    public class RegisterUserService : IRegisterUserService
    {
        private readonly PManagementDbContext _context;

        public RegisterUserService(PManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegisterUserEfo>> GetAllRegisterUsersAsync()
        {
            return await _context.RegisterUsers.ToListAsync();
        }

        public async Task<RegisterUserEfo> GetRegisterUserByIdAsync(int registerUserId)
        {
            RegisterUserEfo? registerUser =  await _context.RegisterUsers.AsNoTracking()
                .FirstOrDefaultAsync(ru => ru.RegisterUserId == registerUserId);

            if (registerUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            return registerUser;
        }

        public async Task<RegisterUserEfo> SendRegisterUserAsync(RegisterUserEfo registerUser)
        {
            await _context.RegisterUsers.AddAsync(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;
        }

        public async Task<RegisterUserEfo> SendLoginUserAsync(string username, string password)
        {
            RegisterUserEfo? loginUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                lu => lu.UserName == username && lu.Password == password);

            return loginUser;
        }

        public async Task<UserEfo> SendNewUserProfileAsync(string username, string password, int registoUserId)
        {
            UserEfo newUserProfile = new UserEfo 
            { 
                UserName = username,
                Password = password,
                RegisterUserId = registoUserId
            };

            await _context.Users.AddAsync(newUserProfile);
            await _context.SaveChangesAsync();

            return newUserProfile;
        }

        public async Task DeleteRegisterUserAsync(int registerUserId)
        {
            RegisterUserEfo? registerUser = await _context.RegisterUsers.FirstOrDefaultAsync(
                ru => ru.RegisterUserId == registerUserId);

            if (registerUser == null)
            {
                throw new Exception("Entity doesn´t exist in the database");
            }

            _context.RegisterUsers.Remove(registerUser);
            await _context.SaveChangesAsync();
        }
    }
}
