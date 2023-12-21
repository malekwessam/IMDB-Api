using IMDB.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMDB.API.Data
{
    public class imdbDbContext : DbContext
    {
        public imdbDbContext(DbContextOptions<imdbDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<WishlistItem> WishlistItem { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<MovieRating> MovieRating { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<Category>()
                .HasMany(b => b.Movie)
                .WithOne(b => b.Category)
                .OnDelete(DeleteBehavior.Cascade);
            
            Builder.Entity<User>()
             .HasMany(b => b.WishlistItem)
             .WithOne(b => b.User)
             .OnDelete(DeleteBehavior.Cascade);
            Builder.Entity<Movie>()
             .HasMany(b => b.WishlistItem)
             .WithOne(b => b.Movie)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}