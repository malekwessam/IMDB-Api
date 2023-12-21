using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IUserService
    {
        Task<bool> IsUserExistAsync(string name);
        Task<User> GetUserAsync(int userId);
        Task<List<User>> GetUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<User> GetUserIdAsync(int userId);
        Task<User> GetUserAndWishlistsAsync(int userId);
    }
}
