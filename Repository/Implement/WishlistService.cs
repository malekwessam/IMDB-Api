using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class WishlistService:IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        public Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem)
        {
            return wishlistRepository.CreateWishlistItemAsync(wishlistItem);
        }

        public Task<bool> DeleteWishlistItemAsync(long id)
        {
            return wishlistRepository.DeleteWishlistItemAsync(id);
        }

        public Task<WishlistItem> GetWishlistItemAsync(int adObjName, int movieId)
        {
            return wishlistRepository.GetWishlistItemAsync(adObjName, movieId);
        }

        public Task<WishlistItem> GetWishlistItemAsync(long id)
        {
            return wishlistRepository.GetWishlistItemAsync(id);
        }

        public Task<List<WishlistItem>> GetWishlistItemsAsync()
        {
            return wishlistRepository.GetWishlistItemsAsync();
        }

        public Task<bool> IsWishlistItemExistAsync(long wishlistItemId)
        {
            return wishlistRepository.IsWishlistItemExistAsync(wishlistItemId);
        }

        public Task<bool> IsWishlistItemExistAsync(int ownerId, int movieId)
        {
            return wishlistRepository.IsWishlistItemExistAsync(ownerId, movieId);
        }
    }
}
