
using RecipeBookApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public interface IIngredientService
    {
        Task<ServiceResponse<IngredientDto>> AddIngredient(IngredientDto newRecipe);
        Task<ServiceResponse<IngredientDto>> GetIngredient(long ingredientId);
        Task<List<IngredientDto>> GetIngredients();

    }
}
