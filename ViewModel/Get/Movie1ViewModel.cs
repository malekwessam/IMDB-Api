using IMDB.API.Validation;
using System;

namespace IMDB.API.ViewModel.Get
{
    public class Movie1ViewModel : AbstractValidatableObject
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descriptions { get; set; }
        public string pathVideo { get; set; }


        public DateTime AvailableSince { get; set; }
        
        public short CategoryId { get; set; }

        public int ActorId { get; set; }
    }
}
