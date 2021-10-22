using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Dto
{
    /// <summary>
    /// RecipeIngredient DTO
    /// </summary>
    public class RecipeIngredientDto
    {
        public long RecipeId { get; set; }
        public long IngredientId { get; set; }
    }
}
