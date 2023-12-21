using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class MovieRatingRepository:IMovieRatingRepository
    {
        private readonly imdbDbContext DbContext;
        public MovieRatingRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<bool> SaveRatingAsync(MovieRating movieRating)
        {
            var rating = DbContext.MovieRating.FirstOrDefault(mr => mr.MovieId == movieRating.MovieId && mr.UserId == movieRating.UserId);

            if (rating != null)
            {
                rating.Rating = movieRating.Rating;
            }
            else
            {
                DbContext.MovieRating.Add(movieRating);
            }

            return await DbContext.SaveChangesAsync() > 0;
        }
    }
}
