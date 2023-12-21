using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMDB.API.Repository.Implement
{
    public class MovieRatingService:IMovieRatingService
    {
        private readonly IMovieRatingRepository movieRatingRepository;
        public MovieRatingService(IMovieRatingRepository movieRatingRepository)
        {
            this.movieRatingRepository = movieRatingRepository;
        }

        public Task<bool> SaveRatingAsync(MovieRating movieRating)
        {
            return movieRatingRepository.SaveRatingAsync(movieRating);
        }
       
    }
}
