using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace IMDB.API.Entities
{
    public class Movie
    {
        public Movie()
        {
            WishlistItem = new HashSet<WishlistItem>();
            MovieRatings = new HashSet<MovieRating>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(250)]
        [Column(TypeName = "nvarchar(250)")]
        public string MovieName { get; set; }

        [MinLength(1), MaxLength(8000)]
        [Column(TypeName = "nvarchar(max)")]
        public string? MovieDescription { get; set; }

        public DateTime? AvailableSince { get; set; }
        public DateTime? CreatedDate { get; set; }
        [MaxLength(200)]
        
        public int NumberOfQuantity { get; set; }
        

        public short? CategoryId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string PathImage { get; set; }
        
        public  string Pathvideo { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AverageRating { get; set; }


        public int? ActorId { get; set; }


        public virtual Category Category { get; set; }
        public virtual Actor Actor { get; set; }
        public virtual ICollection<WishlistItem> WishlistItem { get; set; }
        public virtual ICollection<MovieRating> MovieRatings { get; set; }
    }
}
