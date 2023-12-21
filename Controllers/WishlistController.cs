using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using IMDB.API.ViewModel.Create;
using IMDB.API.ViewModel.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        // GET: api/<WishlistController>
        private readonly IWishlistService wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            this.wishlistService = wishlistService;
        }
        [HttpGet("all", Name = "GetWishlists")]
        [ProducesResponseType(typeof(List<WishlistItemViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWishlistItemsAsync()
        {

            var wishlists = await wishlistService.GetWishlistItemsAsync();

            var wishlistsViewModel = wishlists.Select(s => new WishlistItemViewModel()
            {
                UserId = (int)s.UserId,
                MovieId = Convert.ToInt32(s.MovieId),
                Id = s.Id,

            }).ToList();

            return Ok(wishlistsViewModel);
        }
        [HttpPost("", Name = "CreateWishlist")]
        public async Task<IActionResult> PostWishlistAsync([FromBody] CreateWishlistItem createWishlistItem)
        {

            var wishListInDB = await wishlistService.GetWishlistItemAsync(createWishlistItem.UserId,
                createWishlistItem.MovieId);

            if (wishListInDB == null)
            {
                var entity = new WishlistItem()
                {
                    UserId = createWishlistItem.UserId,
                    MovieId = createWishlistItem.MovieId
                };

                var isSuccess = await wishlistService.CreateWishlistItemAsync(entity);
                return new CreatedAtRouteResult("GetWishlist",
                  new { id = entity.Id });
            }
            return new CreatedAtRouteResult("GetWishlist",
                   new { id = wishListInDB.Id });
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(long id)
        {
            var product = await wishlistService.GetWishlistItemAsync(id);
            if (product == null)
                return NotFound();

           
            var isSuccess = await wishlistService.DeleteWishlistItemAsync(id);
            return Ok();
        }
    }
}
