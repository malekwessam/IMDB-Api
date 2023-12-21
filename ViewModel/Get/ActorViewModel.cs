using IMDB.API.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.API.ViewModel.Get
{
    public class ActorViewModel:AbstractValidatableObject
    {
        public int Id { get; set; }
       
        public string ActorName { get; set; }
       
        public string ActorDescription { get; set; }
        public string ActorNationality { get; set; }

        public List<ActorMovieViewModel> ActorMovieViewModels { get; set; }
    }
}
