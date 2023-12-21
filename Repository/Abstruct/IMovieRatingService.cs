using IMDB.API.Entities;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IMovieRatingService
    {
        Task<bool> SaveRatingAsync(MovieRating movieRating);
    }
}
