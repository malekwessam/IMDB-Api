using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using IMDB.API.ViewModel.Create;
using IMDB.API.ViewModel.Get;
using IMDB.API.ViewModel.Update;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService actorService;

        private readonly IHostingEnvironment hostingEnvironment;

        public ActorController(IActorService actorService, IHostingEnvironment hostingEnvironment)
        {
            this.actorService = actorService;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: api/<UserController>
        [HttpGet("", Name = "GetActors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ActorViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var actors = await actorService.GetActorsAsync();

            var models = actors.Select(actor => new ActorViewModel()
            {

                Id = actor.Id,
                ActorName = actor.ActorName == null ? null : actor.ActorName,
                ActorDescription = actor.ActorDescription == null ? null : actor.ActorDescription,
                ActorNationality = actor.ActorNationality == null ? null : actor.ActorNationality



            }).ToList();
            return Ok(models);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetActor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActorViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var actor = await actorService.GetActorAndMoviesAsync(id);
            if (actor == null)
                return NotFound();


            var model = new ActorViewModel()
            {
                Id = actor.Id,
                ActorName = actor.ActorName == null ? null : actor.ActorName,
                ActorDescription = actor.ActorDescription == null ? null : actor.ActorDescription,
                ActorNationality = actor.ActorNationality == null ? null : actor.ActorNationality,




                ActorMovieViewModels = actor.Movie.Any() ? actor.Movie.Select(s => new ActorMovieViewModel()
                {


                    Id = s.Id,
                    Name = s.MovieName,
                    Descriptions = s.MovieDescription,
                    pathImage = s.PathImage,
                    AvailableSince = (System.DateTime)s.AvailableSince,

                    CategoryId = (short)s.CategoryId,
                    ActorId = actor.Id

                }).ToList() : new List<ActorMovieViewModel>()

            };
            return Ok(model);
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateActor createActor)
        {

            var entityToAdd = new Actor()
            {
                Id = createActor.Id,
                ActorName = createActor.ActorName,
                ActorDescription = createActor.ActorDescription,
                ActorNationality = createActor.ActorNationality,

            };
            var createdActor = await actorService.CreateActorAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createActor.Id });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateActor updateActor)
        {



            var entityToUpdate = await actorService.GetActorAndMoviesAsync(updateActor.Id);


            entityToUpdate.Id = updateActor.Id;
            entityToUpdate.ActorName = updateActor.ActorName;
            entityToUpdate.ActorDescription = updateActor.ActorDescription;
            entityToUpdate.ActorNationality = updateActor.ActorNationality;

            var updatedCategory = await actorService.UpdateActorAsync(entityToUpdate);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await actorService.GetActorAndMoviesAsync(id);
            if (user == null)
                return NotFound();

            var isSuccess = await actorService.DeleteActorAsync(id);

            return Ok();
        }
    }
}
