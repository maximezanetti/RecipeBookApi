using System.Collections.Generic;

namespace RecipeBookApi.Models
{
    public class Ingredient
    {
        public long IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }

        public double IngredientAmount { get; set; }
        public string IngredientMeasurementType { get; set; }
        public ICollection<RecipeIngredients> RecipeIngredients { get; set; }
    }
}
