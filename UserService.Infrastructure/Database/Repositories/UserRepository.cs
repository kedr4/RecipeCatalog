using Microsoft.EntityFrameworkCore;
using UserService.Domain.Users;

namespace UserService.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> CreateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<string> UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
