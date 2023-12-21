using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Task<Movie> CreateMovieAsync(Movie movie)
        {
            return movieRepository.CreateMovieAsync(movie);
        }

        public Task<bool> DeleteMovieAsync(int movieId)
        {
            return movieRepository.DeleteMovieAsync(movieId);
        }

        public Movie GetMovie(int movieId)
        {
            return movieRepository.GetMovie(movieId);
        }

        public Task<Movie> GetMovieAsync(int movieId)
        {
            return movieRepository.GetMovieAsync(movieId);
        }

        public List<Movie> GetMovies(int noOfMovies = 100)
        {
            return movieRepository.GetMovies(noOfMovies);
        }

        public Task<List<Movie>> GetMoviesAsync(int noOfMovies = 100)
        {
            return movieRepository.GetMoviesAsync(noOfMovies);
        }

        public async Task<bool> IsMovieExistAsync(string name, int id)
        {
            var movie = await movieRepository.GetMovieByNameAsync(name, id);
            return movie != null;
        }

        public async Task<bool> IsMovieNameExistAsync(string name)
        {
            var product = await movieRepository.GetMovieByNameAsync(name);
            return product != null;
        }

        public Task<Movie> UpdateMovieAsync(Movie movie)
        {
            return movieRepository.UpdateMovieAsync(movie);
        }
    }
}
