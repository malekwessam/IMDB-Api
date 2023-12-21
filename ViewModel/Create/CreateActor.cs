using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using IMDB.API.ViewModel.Get;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.API.ViewModel.Create
{
    public class CreateActor:ActorViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
  CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var actorService = validationContext.GetService<IActorService>();

            if (await actorService.IsActorExistAsync(ActorName))
            {
                errors.Add(new ValidationResult($"Actor name {ActorName} exist", new[] { nameof(ActorName) }));
            }

            return errors;

        }
    }
}
