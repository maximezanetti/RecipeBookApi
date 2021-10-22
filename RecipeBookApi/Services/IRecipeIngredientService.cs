using RecipeBookApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public interface IRecipeIngredientService
    {
        Task<ServiceResponse<RecipeDto>> AddRecipeIngredient(RecipeIngredientDto newRecipeIngredient);
        Task<ServiceResponse<RecipeDto>> DeleteByRecipeId(long id);

    }
}
