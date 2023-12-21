using System;

namespace IMDB.API.ViewModel.Get
{
    public class CategoryMovieViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descriptions { get; set; }
        public string pathImage { get; set; }

        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; }
        public short CategoryId { get; set; }
        public decimal Rating { get; set; }
        public int ActorId { get; set; }
    }
}
