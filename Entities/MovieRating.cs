namespace IMDB.API.Entities
{
    public class MovieRating
    {
        
            public int Id { get; set; }
            public int UserId { get; set; }
            public int MovieId { get; set; }
            public byte Rating { get; set; }

            public virtual Movie Movie { get; set; }
            public virtual User User { get; set; }
        
    }
}
