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
using Microsoft.AspNetCore.Hosting;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        private readonly IHostingEnvironment hostingEnvironment;

        public CategoryController(ICategoryService categoryService, IHostingEnvironment hostingEnvironment)
        {
            this.categoryService = categoryService;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: api/<CategoryController>
        [HttpGet("", Name = "GetCategorys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {

            var categorys = await categoryService.GetCategorysAsync();

            var models = categorys.Select(category => new CategoryViewModel()
            {

                Id = category.Id,
                Name = category.CategoryName == null ? null : category.CategoryName,
               
                pathImage = category.PathImage == null ? null :  category.PathImage,
               


            }).ToList();
            return Ok(models);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(short id)

        {

            var category = await categoryService.GetCategoryAndMoviesAsync(id);
            if (category == null)
                return NotFound();
            

            var model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.CategoryName == null ? null : category.CategoryName,
                pathImage = category.PathImage == null ? null : category.PathImage,
                
                CategoryMovieViewModels = category.Movie.Any() ? category.Movie.Select(s => new CategoryMovieViewModel()
                {


                    Id = s.Id,
                    Name = s.MovieName,
                    Descriptions = s.MovieDescription,
                    pathImage =  s.PathImage,
                    AvailableSince = (DateTime)s.AvailableSince,
                  
                    CategoryId = category.Id,
                    ActorId = (int)s.ActorId
                }).ToList() : new List<CategoryMovieViewModel>()

            };
            return Ok(model);
        }

        // POST api/<CategoryController>
        [HttpPost]

        public async Task<ActionResult> Post([FromForm] CreateCategory createCategory, IFormFile image)
        {
            Random random = new Random();
            int rNum = random.Next();
            var images = "images/" + rNum + image.FileName;
            var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
            var streamImage = new FileStream(pathImage, FileMode.Append);
            image.CopyTo(streamImage);
            var entityToAdd = new Category()
            {
                //Id=createCategory.Id,
                CategoryName = createCategory.Name,
                PathImage = images,
                

            };
            var createdProduct = await categoryService.CreateCategoryAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createCategory.Id });
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}", Name = "UpdateCategory")]

        public async Task<ActionResult> Put(short id, [FromForm] UpdateCategory updateCategory, IFormFile image)
        {



            string images = null;
            if (image != null)
            {
                Random random = new Random();
                int rNum = random.Next();
                images = "images/" + rNum + image.FileName;
                var pathImage = Path.Combine(hostingEnvironment.WebRootPath, images);
                var streamImage = new FileStream(pathImage, FileMode.Append);
                image.CopyTo(streamImage);
            }

            var entityToUpdate = await categoryService.GetCategoryAsync(updateCategory.Id);


            entityToUpdate.Id = updateCategory.Id;
            entityToUpdate.CategoryName = updateCategory.Name;

           

            entityToUpdate.PathImage = images;
            

            var updatedCategory = await categoryService.UpdateCategoryAsync(entityToUpdate);
            return Ok();

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(short id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category == null)
                return NotFound();
            
            var isSuccess = await categoryService.DeleteCategoryAsync(id);

            return Ok();
        }
    }
}
