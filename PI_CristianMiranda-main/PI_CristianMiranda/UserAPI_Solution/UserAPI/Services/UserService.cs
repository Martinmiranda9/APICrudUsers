using UserAPI.Models;
using UserAPI.Repositories;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await UserRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await UserRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await UserRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await UserRepository.DeleteAsync(id);
        }

    }
}
