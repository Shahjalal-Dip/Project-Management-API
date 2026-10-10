using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string username) =>
            await _context.Users.AnyAsync(u => u.Username == username);
    }

}
