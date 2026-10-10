using ProjectManagementApp.Models;

namespace ProjectManagementApp.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task<bool> ExistsAsync(string username);
    }

}
