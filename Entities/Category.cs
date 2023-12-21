using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMDB.API.Entities
{
    public class Category
    {
        public Category()
        {
            Movie = new HashSet<Movie>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }
        [Required]
        [MinLength(4), MaxLength(100)]
        [Column(TypeName = "nvarchar(250)")]
        public string CategoryName { get; set; }

        
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string PathImage { get; set; }

        

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
