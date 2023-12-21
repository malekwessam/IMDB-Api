using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class UserRepository: IUserRepository
    {
        private readonly imdbDbContext DbContext;
        public UserRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            DbContext.User.Add(user);
            await DbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var userToRemove = await DbContext.User.FindAsync(userId);
            DbContext.User.Remove(userToRemove);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<User> GetUserAndWishlistsAsync(int userId)
        {
            return DbContext.User.AsNoTracking().Include(i => i.WishlistItem).FirstOrDefaultAsync(f => f.Id == userId);
        }

        public Task<User> GetUserAsync(string name)
        {
            return this.DbContext.User.FirstOrDefaultAsync(f => f.firstName == name.ToLower());
        }

        public Task<User> GetuserAsync(int userId)
        {
            return this.DbContext.User.FindAsync(userId).AsTask();
        }

        public Task<User> GetuserIdAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserIdAsync(int userId)
        {
            return DbContext.User.AsNoTracking().Include(i => i.WishlistItem).FirstOrDefaultAsync(f => f.Id == userId);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return this.DbContext.User.ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            DbContext.User.Update(user);
            await DbContext.SaveChangesAsync();
            return user;
        }
    }
}
