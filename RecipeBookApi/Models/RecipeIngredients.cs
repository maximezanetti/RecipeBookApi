using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    public class RecipeIngredients
    {
        //[ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public long RecipeId { get; set; }

        //[ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }        
        public long IngredientId { get; set; }
    }
}
