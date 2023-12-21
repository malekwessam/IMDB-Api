using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class MovieRepository : IMovieRepository
    {
        private readonly imdbDbContext DbContext;
        public MovieRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            DbContext.Movie.Add(movie);
            await DbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteMovieAsync(int movieId)
        {
            var movieToRemove = await DbContext.Movie.FindAsync(movieId);
            DbContext.Remove(movieToRemove);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public Movie GetMovie(int movieId)
        {
            return this.DbContext.Movie.Find(movieId);
        }

        public Task<Movie> GetMovieAsync(int movieId)
        {
            return this.DbContext.Movie.FindAsync(movieId).AsTask();
        }

        public Task<Movie> GetMovieByNameAsync(string name)
        {
            return this.DbContext.Movie.FirstOrDefaultAsync(f => f.MovieName.ToLower() == name.ToLower());
        }

        public Task<Movie> GetMovieByNameAsync(string name, int id)
        {
            return DbContext.Movie.AsNoTracking().FirstOrDefaultAsync(f => f.MovieName.ToLower() == name.ToLower() && f.Id != id);
        }

        public List<Movie> GetMovies(int noOfMovies)
        {
            var movies = this.DbContext.Movie.OrderByDescending(o => o.CreatedDate).Take(noOfMovies).ToList();
            return movies;
        }

        public Task<List<Movie>> GetMoviesAsync(int noOfMovies)
        {
            var movies = DbContext.Movie.OrderByDescending(o => o.CreatedDate).Take(noOfMovies).ToListAsync();
            return movies;
        }

        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            DbContext.Movie.Update(movie);
            await DbContext.SaveChangesAsync();
            return movie;
        }
    }
}
