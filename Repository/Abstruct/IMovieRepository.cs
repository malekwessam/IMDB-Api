using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IMovieRepository
    {
        Movie GetMovie(int movieId);
        List<Movie> GetMovies(int noOfMovies);

        Task<Movie> GetMovieAsync(int movieId);
        Task<List<Movie>> GetMoviesAsync(int noOfMovies);

        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int movieId);
        Task<Movie> GetMovieByNameAsync(string name);
        Task<Movie> GetMovieByNameAsync(string name, int id);
    }
}
