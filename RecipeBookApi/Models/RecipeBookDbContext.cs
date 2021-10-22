using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Models;

namespace RecipeBookApi.Context
{
    public class RecipeBookDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }


        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().ToTable("Recipes");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<RecipeIngredients>().ToTable("RecipeIngredients");

            modelBuilder.Entity<RecipeIngredients>().HasKey(x => new { x.RecipeId, x.IngredientId });

            modelBuilder.Entity<RecipeIngredients>()
                .HasOne(m => m.Recipe)
                .WithMany(ma => ma.RecipeIngredients)
                .HasForeignKey(m => m.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RecipeIngredients>()
                .HasOne(m => m.Ingredient)
                .WithMany(ma => ma.RecipeIngredients)
                .HasForeignKey(a => a.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
