using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface IActorService
    {
        Task<bool> IsActorExistAsync(string name);
        Task<Actor> GetActorAsync(int actorId);
        Task<List<Actor>> GetActorsAsync();
        Task<Actor> CreateActorAsync(Actor actor);
        Task<Actor> UpdateActorAsync(Actor actor);
        Task<bool> DeleteActorAsync(int actorId);
        Task<Actor> GetActorAndMoviesAsync(int actorId);
    }
}
