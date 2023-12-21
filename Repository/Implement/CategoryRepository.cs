using IMDB.API.Data;
using IMDB.API.Entities;
using IMDB.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.API.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly imdbDbContext DbContext;
        public CategoryRepository(imdbDbContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            DbContext.Category.Add(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeleteCategoryAsync(short categoryId)
        {
            // this will return entity and that is tracked
            var categoryToRemove = await DbContext.Category.FindAsync(categoryId);
            DbContext.Category.Remove(categoryToRemove);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<Category> GetCategoryAndMoviesAsync(short categoryId)
        {
            return DbContext.Category.AsNoTracking().Include(i => i.Movie).FirstOrDefaultAsync(f => f.Id == categoryId);
        }

        public Task<Category> GetCategoryAsync(short categoryId)
        {
            return this.DbContext.Category.FindAsync(categoryId).AsTask();
        }

        public Task<Category> GetCategoryAsync(string name)
        {
            return this.DbContext.Category.FirstOrDefaultAsync(f => f.CategoryName.ToLower() == name.ToLower());
        }
        public Task<List<Category>> GetCategorysAsync()
        {
            return this.DbContext.Category.ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            DbContext.Category.Update(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
    }
}
