using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task<User> CreateUserAsync(User user)
        {
            return userRepository.CreateUserAsync(user);
        }

        public Task<bool> DeleteUserAsync(int userId)
        {
            return userRepository.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserAndWishlistsAsync(int userId)
        {
            return await userRepository.GetUserAndWishlistsAsync(userId);
        }

        public Task<User> GetUserAsync(int userId)
        {
            return userRepository.GetuserAsync(userId);
        }

        public async Task<User> GetUserIdAsync(int userId)
        {
            return await userRepository.GetUserIdAsync(userId);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return userRepository.GetUsersAsync();
        }

        public async Task<bool> IsUserExistAsync(string name)
        {
            var user = await userRepository.GetUserAsync(name);
            return user != null;
        }

        public Task<User> UpdateUserAsync(User user)
        {
            return userRepository.UpdateUserAsync(user);
        }
    }
}
