using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IWishlistService
    {
        Task<List<WishlistItem>> GetWishlistItemsAsync();
        Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem);
        Task<bool> IsWishlistItemExistAsync(long wishlistItemId);
        Task<bool> IsWishlistItemExistAsync(int ownerId, int movieId);
        Task<bool> DeleteWishlistItemAsync(long id);
        Task<WishlistItem> GetWishlistItemAsync(int adObjName, int movieId);
        Task<WishlistItem> GetWishlistItemAsync(long id);
    }
}
