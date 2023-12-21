using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using IMDB.API.ViewModel.Get;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.API.ViewModel.Create
{
    public class CreateWishlistItem : WishlistItemViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
        CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var wishlistService = validationContext.GetService<IWishlistService>();
            var userService = validationContext.GetService<IUserService>();
            var user = await userService.GetUserAsync(UserId);

            if (await wishlistService.IsWishlistItemExistAsync(UserId, MovieId))
            {
                errors.Add(new ValidationResult($"Movie id {MovieId} exist for owner {UserId}", new[] { nameof(MovieId) }));
            }
            if (user == null)
            {
                errors.Add(new ValidationResult($"user id {UserId} doesn't exist", new[] { nameof(UserId) }));
            }
            return errors;

        }
    }
}
