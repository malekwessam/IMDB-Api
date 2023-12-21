using IMDB.API.Repository.Abstruct;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using IMDB.API.ViewModel.Get;

namespace IMDB.API.ViewModel.Create
{
    public class CreateUser: UserViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
  CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var userService = validationContext.GetService<IUserService>();

            if (await userService.IsUserExistAsync(firstName))
            {
                errors.Add(new ValidationResult($"Category name {firstName} exist", new[] { nameof(firstName) }));
            }

            return errors;

        }
    }
}
