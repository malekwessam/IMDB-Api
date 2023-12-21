using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMDB.API.Entities
{
    public class User
    {
        public User()
        {
            WishlistItem = new HashSet<WishlistItem>();
            MovieRatings = new HashSet<MovieRating>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        public string phoneNumber { get; set; }
        [Required]
        public string country { get; set; }
  
    
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        
        [Column(TypeName = "nvarchar(max)")]
        public string PathImage { get; set; }


        
        public virtual ICollection<WishlistItem> WishlistItem { get; set; }
        public virtual ICollection<MovieRating> MovieRatings { get; set; }
    }
}
