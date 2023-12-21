using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IMDB.API.Entities
{
    public class Actor
    {
        public Actor()
        {
            Movie = new HashSet<Movie>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ActorName { get; set; }
        [Required]
        public string ActorDescription { get; set; }
        [Required]
        public string ActorNationality { get; set; }
        

        public virtual ICollection<Movie> Movie { get; set; }

    }
}
