using IMDB.API.Repository.Abstruct;
using IMDB.API.ViewModel.Create;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using IMDB.API.ViewModel.Get;

namespace IMDB.API.ViewModel.Update
{
    public class UpdateMovie:Movie2ViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
    CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var categoryService = validationContext.GetService<ICategoryService>();



            var category = await categoryService.GetCategoryAsync(CategoryId);


            if (category == null)
            {
                errors.Add(new ValidationResult($"Category id {CategoryId} doesn't exist", new[] { nameof(CategoryId) }));
            }



            return errors;
        }
    }
}
