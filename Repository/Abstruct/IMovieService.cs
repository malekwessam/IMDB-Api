using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IMovieService
    {
        Movie GetMovie(int movieId);
        List<Movie> GetMovies(int noOfMovies = 100);

        Task<Movie> GetMovieAsync(int movieId);
        Task<List<Movie>> GetMoviesAsync(int noOfMovies = 100);
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int movieId);
        Task<bool> IsMovieNameExistAsync(string name);
        Task<bool> IsMovieExistAsync(string name, int id);
    }
}
