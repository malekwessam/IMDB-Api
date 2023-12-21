using IMDB.API.Validation;
using System.Collections.Generic;

namespace IMDB.API.ViewModel.Get
{
    public class CategoryViewModel : AbstractValidatableObject
    {
        public short Id { get; set; }
        public string Name { get; set; }
       
        public string pathImage { get; set; }

        
        public List<CategoryMovieViewModel> CategoryMovieViewModels { get; set; }
    }
}
