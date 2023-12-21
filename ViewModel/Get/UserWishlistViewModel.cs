using System.ComponentModel.DataAnnotations;

namespace IMDB.API.ViewModel.Get
{
    public class UserWishlistViewModel
    {
        public long Id { get; set; }
        public int MovieId { get; set; }
        
    }
}
