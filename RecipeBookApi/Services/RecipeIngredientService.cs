using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Context;
using RecipeBookApi.Dto;
using RecipeBookApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly RecipeBookDbContext _context;

        public RecipeIngredientService(RecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<RecipeDto>> AddRecipeIngredient(RecipeIngredientDto newRecipeIngredient)
        {
            ServiceResponse<RecipeDto> response = new ServiceResponse<RecipeDto>();

            try
            {
                Recipe recipe = await _context.Recipes
                    .Include(c => c.RecipeIngredients)
                    .ThenInclude(cs => cs.Ingredient)
                    .FirstOrDefaultAsync(c => c.RecipeId == newRecipeIngredient.RecipeId);

                if (recipe == null)
                {
                    response.Success = false;
                    response.Message = "Recipe not found.";
                    return response;
                }

                Ingredient ingredient = await _context.Ingredients
                    .FirstOrDefaultAsync(s => s.IngredientId == newRecipeIngredient.IngredientId);

                if (ingredient == null)
                {
                    response.Success = false;
                    response.Message = "Ingredient not found.";
                    return response;
                }

                RecipeIngredients recipeIngredient = new RecipeIngredients
                {
                    Recipe = recipe,
                    Ingredient = ingredient
                };

                await _context.RecipeIngredients.AddAsync(recipeIngredient);
                await _context.SaveChangesAsync();
                //response.Data = _mapper.Map<RecipeDto>(recipe);
                response.Success = true;
                response.Message = "Recipe and Ingredients found";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<RecipeDto>> DeleteByRecipeId(long id)
        {
            ServiceResponse<RecipeDto> response = new ServiceResponse<RecipeDto>();

            try
            {
                var recipeIngredient = await _context.RecipeIngredients.Where(ri => ri.RecipeId == id).ToListAsync();

                if (recipeIngredient == null)
                {
                    response.Success = false;
                    response.Message = "Recipe ingredients do not exist.";
                }

                foreach(var ri in recipeIngredient)
                {
                    var boolTest = _context.RecipeIngredients.Remove(ri);
                }

                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Recipe Ingredients deleted";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
