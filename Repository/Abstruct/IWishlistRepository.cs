using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IWishlistRepository
    {
        Task<List<WishlistItem>> GetWishlistItemsAsync();
        Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem);
        Task<bool> IsWishlistItemExistAsync(long wishlistItemId);
        Task<bool> DeleteWishlistItemAsync(long id);
        Task<bool> IsWishlistItemExistAsync(int ownerId, int movieId);
        Task<WishlistItem> GetWishlistItemAsync(int adObjName, int movieId);
        Task<WishlistItem> GetWishlistItemAsync(long id);
    }
}
