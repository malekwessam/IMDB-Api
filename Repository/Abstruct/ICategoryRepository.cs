using IMDB.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Abstruct
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(string name);

        Task<Category> GetCategoryAsync(short categoryId);
        Task<List<Category>> GetCategorysAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(short categoryId);
        Task<Category> GetCategoryAndMoviesAsync(short categoryId);
    }
}
