using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using IMDB.API.ViewModel.Create;
using IMDB.API.ViewModel.Get;
using IMDB.API.ViewModel.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly IHostingEnvironment hostingEnvironment;

        public UserController(IUserService userService, IHostingEnvironment hostingEnvironment)
        {
            this.userService = userService;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: api/<UserController>
        [HttpGet("", Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var users = await userService.GetUsersAsync();

            var models = users.Select(user => new UserViewModel()
            {

                Id = user.Id,
                firstName = user.firstName == null ? null : user.firstName,
                lastName = user.lastName == null ? null : user.lastName,
                PathImage = user.PathImage == null ? null : user.PathImage,
                country = user.country == null ? null : user.country,

                phoneNumber = user.phoneNumber == null ? null : user.phoneNumber


            }).ToList();
            return Ok(models);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var user = await userService.GetUserAndWishlistsAsync(id);
            if (user == null)
                return NotFound();


            var model = new UserViewModel()
            {
                Id = user.Id,
                firstName = user.firstName == null ? null : user.firstName,
                lastName = user.lastName == null ? null : user.lastName,
                PathImage = user.PathImage == null ? null : user.PathImage,
                country = user.country == null ? null : user.country,

                phoneNumber = user.phoneNumber == null ? null : user.phoneNumber,
                UserWishlistViewModels = user.WishlistItem.Any() ? user.WishlistItem.Select(s => new UserWishlistViewModel()
                {
                    Id = s.Id,
                    MovieId = (int)s.MovieId
                }).ToList() : new List<UserWishlistViewModel>()


            };
            return Ok(model);
        }
      


        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateUser createUser, IFormFile image)
        {
            Random random = new Random();
            int rNum = random.Next();
            var images = "UImages/" + rNum + image.FileName;
            var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
            var streamImage = new FileStream(pathImage, FileMode.Append);
            image.CopyTo(streamImage);
            var entityToAdd = new User()
            {
                Id = createUser.Id,
                firstName = createUser.firstName,
                lastName = createUser.lastName,
                password = createUser.password,
                email = createUser.email,
                PathImage = images,
                country = createUser.country,

                phoneNumber = createUser.phoneNumber,

            };
            var createdUser = await userService.CreateUserAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createUser.Id });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateUser updateUser, IFormFile image)
        {

            string images = null;
            if (image != null)
            {
                Random random = new Random();
                int rNum = random.Next();
                images = "UImages/" + rNum + image.FileName;
                var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
                var streamImage = new FileStream(pathImage, FileMode.Append);
                image.CopyTo(streamImage);
            }
            var entityToUpdate = await userService.GetUserAsync(updateUser.Id);


            entityToUpdate.Id = updateUser.Id;
            entityToUpdate.firstName = updateUser.firstName;
            entityToUpdate.lastName = updateUser.lastName;
            entityToUpdate.password = updateUser.password;
            entityToUpdate.email = updateUser.email;
            entityToUpdate.PathImage = updateUser.PathImage;
            entityToUpdate.country = updateUser.country;

            entityToUpdate.phoneNumber = updateUser.phoneNumber;

            var updatedCategory = await userService.UpdateUserAsync(entityToUpdate);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userService.GetUserAsync(id);
            if (user == null)
                return NotFound();

            var isSuccess = await userService.DeleteUserAsync(id);

            return Ok();
        }
    }
}
