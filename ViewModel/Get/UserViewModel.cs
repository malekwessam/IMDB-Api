using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IMDB.API.Validation;
using System.Collections.Generic;

namespace IMDB.API.ViewModel.Get
{
    public class UserViewModel:AbstractValidatableObject
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string country { get; set; }
       

        public string email { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string PathImage { get; set; }

        public List<UserWishlistViewModel> UserWishlistViewModels { get; set; }
    }
}
