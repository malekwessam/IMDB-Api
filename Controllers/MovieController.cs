using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using IMDB.API.ViewModel.Create;
using IMDB.API.ViewModel.Get;
using IMDB.API.ViewModel.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;
        [Obsolete]
        public MovieController(IMovieService movieService, IHostingEnvironment hostingEnvironment)
        {
            this.movieService = movieService;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: api/<ProductController>
        [HttpGet("", Name = "GetMovies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<MovieViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {

            var movies = await movieService.GetMoviesAsync();
            var models = movies.Select(movie => new MovieViewModel()
            {
                Id = movie.Id,
                Name = movie.MovieName == null ? null : movie.MovieName,
                pathImage = movie.PathImage == null ? null : movie.PathImage,
                Descriptions = movie.MovieDescription == null ? null : movie.MovieDescription,
                AvailableSince = (DateTime)movie.AvailableSince,

                CategoryId = Convert.ToInt16(movie.CategoryId),

                ActorId = (int)(movie.ActorId == null ? null : movie.ActorId)
            }).ToList();
            return Ok(models);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}", Name = "GetMovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Movie1ViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var movie = await movieService.GetMovieAsync(id);
            if (movie == null)
                return NotFound();

            var model = new Movie1ViewModel()
            {

                Id = movie.Id,
                Name = movie.MovieName == null ? null : movie.MovieName,
                pathVideo = movie.Pathvideo == null ? null : movie.Pathvideo,
                Descriptions = movie.MovieDescription == null ? null : movie.MovieDescription,
                AvailableSince = (DateTime)movie.AvailableSince,

                CategoryId = Convert.ToInt16(movie.CategoryId),
                ActorId = (int)(movie.ActorId == null ? null : movie.ActorId)
            };
            return Ok(model);

        }

        // POST api/<ProductController>
        [HttpPost("", Name = "CreateMovie")]
        public async Task<ActionResult> Post([FromForm] CreateMovie createMovie, IFormFile image, IFormFile video)
        {
            Random random = new Random();
            int rNum = random.Next();
            var images = "PImages/" + rNum + image.FileName;
            var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
            var streamImage = new FileStream(pathImage, FileMode.Append);
            image.CopyTo(streamImage);

            Random random1 = new Random();
            int rNum1 = random.Next();
            var videos = "VImages/" + rNum1 + video.FileName;
            var pathVideo = Path.Combine(hostingEnvironment.WebRootPath, videos);
            var streamVideo = new FileStream(pathVideo, FileMode.Append);
            video.CopyTo(streamVideo);

            var entityToAdd = new Movie()
            {

                MovieName = createMovie.Name,
                PathImage = images,
                MovieDescription = createMovie.Descriptions,
                CreatedDate = DateTime.Now,
                AvailableSince = DateTime.Now,
                Pathvideo=videos,
                CategoryId = createMovie.CategoryId,

                ActorId = createMovie.ActorId,
            };

            var createdProduct = await movieService.CreateMovieAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createMovie.Id });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateMovie updateMovie, IFormFile image)
        {
            string images = null;

            if (image != null)
            {
                Random random = new Random();
                int rNum = random.Next();
                images = "PImages/" + rNum + image.FileName;
                var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
                var streamImage = new FileStream(pathImage, FileMode.Append);
                image.CopyTo(streamImage);


            }

            var entityToUpdate = await movieService.GetMovieAsync(updateMovie.Id);



            entityToUpdate.MovieName = updateMovie.Name;

            entityToUpdate.PathImage = images;
            entityToUpdate.CategoryId = updateMovie.CategoryId;


            entityToUpdate.MovieDescription = updateMovie.Descriptions;


            var updatedMovie = await movieService.UpdateMovieAsync(entityToUpdate);
            return Ok();
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await movieService.GetMovieAsync(id);
            if (product == null)
                return NotFound();


            var isSuccess = await movieService.DeleteMovieAsync(id);
            return Ok();
        }
    }
}
