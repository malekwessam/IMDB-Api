using IMDB.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace IMDB.API.ViewModel.Get
{
    public class WishlistItemViewModel : AbstractValidatableObject
    {
        public long Id { get; set; }
        public int MovieId { get; set; }
        [MaxLength(200)]
        public int UserId { get; set; }
    }
}
