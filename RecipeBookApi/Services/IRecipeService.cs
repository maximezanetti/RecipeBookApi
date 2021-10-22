using RecipeBookApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public interface IRecipeService
    {
        Task<ServiceResponse<RecipeDto>> AddRecipe(RecipeDto newRecipe);
        Task<ServiceResponse<RecipeDto>> GetRecipe(long RecipeId);
        Task<ServiceResponse<RecipeDto>> GetRecipes();
        ServiceResponse<RecipeDto> UpdateRecipe(RecipeDto newRecipeDto);
        Task<RecipeDto> DeleteRecipe(long recipeId);

    }
}
