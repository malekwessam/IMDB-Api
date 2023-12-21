using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using IMDB.API.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.API.ViewModel.Create
{
    public class CreateMovie : AbstractValidatableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descriptions { get; set; }
        public string pathImage { get; set; }
        public string pathVideo { get; set; }

       
        public bool IsActive { get; set; }
        public short CategoryId { get; set; }
        
        public int ActorId { get; set; }



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
