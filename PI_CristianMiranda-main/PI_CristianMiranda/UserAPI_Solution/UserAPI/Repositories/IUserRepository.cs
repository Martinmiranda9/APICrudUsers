using UserAPI.Models;

namespace UserAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task DeleteAsync(User user);
        Task<bool> ExistsAsync(int id);
    }
}
