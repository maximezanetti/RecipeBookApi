using System.Collections.Generic;

namespace RecipeBookApi.Models
{
    public class Recipe
    {
        public long RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfPeople { get; set; }
        public int PreparationTime { get; set; }
        public string Instructions { get; set; }
        public byte[] Image { get; set; }
        public ICollection<RecipeIngredients> RecipeIngredients { get; set; }
    }
}
