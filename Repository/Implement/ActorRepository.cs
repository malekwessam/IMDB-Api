using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class ActorRepository: IActorRepository
    {
        private readonly imdbDbContext DbContext;
        public ActorRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<Actor> CreateActorAsync(Actor actor)
        {
            DbContext.Actor.Add(actor);
            await DbContext.SaveChangesAsync();
            return actor;
        }

        public async Task<bool> DeleteActorAsync(int actorId)
        {
            var actorToRemove = await DbContext.Actor.FindAsync(actorId);
            DbContext.Actor.Remove(actorToRemove);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<Actor> GetActorAndMoviesAsync(int actorId)
        {
            return DbContext.Actor.AsNoTracking().Include(i => i.Movie).FirstOrDefaultAsync(f => f.Id == actorId);
        }

        public Task<Actor> GetActorAsync(string name)
        {
            return this.DbContext.Actor.FirstOrDefaultAsync(f => f.ActorName == name.ToLower());
        }

        public Task<Actor> GetActorAsync(int actorId)
        {
            return this.DbContext.Actor.FindAsync(actorId).AsTask();
        }

        public Task<List<Actor>> GetActorsAsync()
        {
            return this.DbContext.Actor.ToListAsync();
        }

        public async Task<Actor> UpdateActorAsync(Actor actor)
        {
            DbContext.Actor.Update(actor);
            await DbContext.SaveChangesAsync();
            return actor;
        }
    }
}
