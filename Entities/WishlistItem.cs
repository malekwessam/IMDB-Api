using System.ComponentModel.DataAnnotations.Schema;

namespace IMDB.API.Entities
{
    public class WishlistItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int? MovieId { get; set; }
        public int? UserId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
