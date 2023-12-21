using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }
        public Task<Actor> CreateActorAsync(Actor actor)
        {
            return actorRepository.CreateActorAsync(actor);
        }

        public Task<bool> DeleteActorAsync(int actorId)
        {
            return actorRepository.DeleteActorAsync(actorId);
        }

        public async Task<Actor> GetActorAndMoviesAsync(int actorId)
        {
            return await actorRepository.GetActorAndMoviesAsync(actorId);
        }

        public Task<Actor> GetActorAsync(int actorId)
        {
            return actorRepository.GetActorAsync(actorId);
        }

        public Task<List<Actor>> GetActorsAsync()
        {
            return actorRepository.GetActorsAsync();
        }

        public async Task<bool> IsActorExistAsync(string name)
        {
            var actor = await actorRepository.GetActorAsync(name);
            return actor != null;
        }

        public Task<Actor> UpdateActorAsync(Actor actor)
        {
            return actorRepository.UpdateActorAsync(actor);
        }
    }
}
