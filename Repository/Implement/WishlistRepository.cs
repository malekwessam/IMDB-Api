using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class WishlistRepository:IWishlistRepository
    {
        private readonly imdbDbContext DbContext;
        public WishlistRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem)
        {
            DbContext.WishlistItem.Add(wishlistItem);
            await DbContext.SaveChangesAsync();
            return wishlistItem;
        }

        public async Task<bool> DeleteWishlistItemAsync(long id)
        {
            var entityToDelete = await DbContext.WishlistItem.FindAsync(id);
            DbContext.WishlistItem.Remove(entityToDelete);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<WishlistItem> GetWishlistItemAsync(int adObjName, int movieId)
        {
            return DbContext.WishlistItem.FirstOrDefaultAsync(f => f.MovieId == movieId
           && f.UserId == adObjName);
        }

        public Task<WishlistItem> GetWishlistItemAsync(long id)
        {
            return this.DbContext.WishlistItem.FindAsync(id).AsTask();
        }

        public Task<List<WishlistItem>> GetWishlistItemsAsync()
        {
            return DbContext.WishlistItem.ToListAsync();
        }

        public async Task<bool> IsWishlistItemExistAsync(long wishlistItemId)
        {
            var entity = await DbContext.WishlistItem.FindAsync(wishlistItemId);
            return entity != null;
        }

        public async Task<bool> IsWishlistItemExistAsync(int ownerId, int movieId)
        {
            var wishlistItem = await DbContext.WishlistItem.FirstOrDefaultAsync(f => f.MovieId == movieId && f.UserId == ownerId);
            return wishlistItem != null;
        }
    }
}
