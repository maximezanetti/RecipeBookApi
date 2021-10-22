using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Context;
using RecipeBookApi.Dto;
using RecipeBookApi.Helper;
using RecipeBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeBookDbContext _context;
        private readonly IIngredientService _ingredientService;

        public RecipeService(RecipeBookDbContext context, IIngredientService ingredientService)
        {
            _context = context;
            _ingredientService = ingredientService;
        }

        /// <summary>
        /// Add a Recipe in database from a Recipe DTO
        /// </summary>
        /// <param name="newRecipeDto">The Recipe DTO to add in database</param>
        /// <returns>The Recipe DTO added in database</returns>
        public async Task<ServiceResponse<RecipeDto>> AddRecipe(RecipeDto newRecipeDto)
        {
            var response = new ServiceResponse<RecipeDto>();

            try
            {
                var newRecipe = new Recipe
                {
                    Description = newRecipeDto.Description,
                    Instructions = newRecipeDto.Instructions,
                    NumberOfPeople = newRecipeDto.NumberOfPeople,
                    PreparationTime = newRecipeDto.PreparationTime,
                    Title = newRecipeDto.Title,
                    Image = new byte[0xCE]
                };

                var addedRecipe = await _context.Recipes.AddAsync(newRecipe);
                response.Message = "Recipes inserted";
                await _context.SaveChangesAsync();
                newRecipeDto.RecipeId = addedRecipe.Entity.RecipeId;

                response.Success = true;
                response.Data = newRecipeDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Get all Recipes from database.
        /// </summary>
        /// <returns>Return Recipies DTO</returns>
        public async Task<ServiceResponse<RecipeDto>> GetRecipes()
        {
            var response = new ServiceResponse<RecipeDto>();

            try
            {
                var recipes = await _context.Recipes.ToListAsync();
                var ingredients = await _ingredientService.GetIngredients();
                var recipeIngredient = await _context.RecipeIngredients.ToListAsync();

                var listRecipesDto = new List<RecipeDto>();

                foreach (var recipe in recipes)
                {
                    var listRecipeIngredientId = recipeIngredient.Where(ri => ri.RecipeId == recipe.RecipeId).Select(r => r.IngredientId);
                    var ingredientForRecipe = ingredients.Where(i => listRecipeIngredientId.Contains(i.IngredientId)).ToList();
                    listRecipesDto.Add(DtoHelper.GetRecipeDto(recipe, ingredients, recipeIngredient));
                }

                response.Success = true;
                response.Message = "All Recipes found";
                response.Data = listRecipesDto;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Get a Recipe by RecipeId
        /// </summary>
        /// <param name="RecipeId">The RecipeId</param>
        /// <returns>The Recipe DTO</returns>
        public async Task<ServiceResponse<RecipeDto>> GetRecipe(long RecipeId)
        {
            var response = new ServiceResponse<RecipeDto>();

            try
            {
                var recipe = await _context.Recipes.FirstOrDefaultAsync(c => c.RecipeId == RecipeId);
                var ingredients = await _ingredientService.GetIngredients();
                var recipeIngredient = await _context.RecipeIngredients.ToListAsync();

                if (recipe == null)
                {
                    response.Success = false;
                    response.Message = "Recipe not found.";
                    return response;
                }

                var recipeDto = DtoHelper.GetRecipeDto(recipe, ingredients, recipeIngredient);

                response.Success = true;
                response.Message = $"Recipe found with id {recipe.RecipeId}";
                response.Data = recipeDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Update an existing Recipe and his Ingredients.
        /// </summary>
        /// <param name="updatedRecipeDto">The updated Recipte DTO</param>
        /// <returns>The new updated Recipe DTO</returns>
        public ServiceResponse<RecipeDto> UpdateRecipe(RecipeDto updatedRecipeDto)
        {
            var response = new ServiceResponse<RecipeDto>();

            try
            {
                var recipeToUpdate = _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == updatedRecipeDto.RecipeId);

                if(recipeToUpdate.Result == null)
                {
                    response.Success = false;
                    response.Message = "Recipe does not exist";
                    return response;
                }

                recipeToUpdate.Result.Description = updatedRecipeDto.Description;
                recipeToUpdate.Result.Instructions = updatedRecipeDto.Instructions;
                recipeToUpdate.Result.NumberOfPeople = updatedRecipeDto.NumberOfPeople;
                recipeToUpdate.Result.PreparationTime = updatedRecipeDto.PreparationTime;
                recipeToUpdate.Result.Title = updatedRecipeDto.Title;

                response.Message = "Recipes updated";
                _context.SaveChanges();
                response.Success = true;
                response.Data = updatedRecipeDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<RecipeDto> DeleteRecipe(long recipeId)
        {
            var recipeDto = new RecipeDto();
            try
            {
                var toBeDeleted = await _context.Recipes.FindAsync(recipeId);

                if(toBeDeleted.RecipeIngredients != null)
                {
                    var listIngredientId = toBeDeleted.RecipeIngredients.Select(ri => ri.IngredientId).ToList();
                    var ingredientsToDelete = _context.Ingredients.Where(i => listIngredientId.Contains(i.IngredientId)).ToList();
                    _context.Ingredients.RemoveRange(ingredientsToDelete);

                }

                recipeDto = DtoHelper.ConvertRecipeToDto(_context.Recipes.Remove(toBeDeleted).Entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Log Exception
            }

            return recipeDto;
        }
    }
}
