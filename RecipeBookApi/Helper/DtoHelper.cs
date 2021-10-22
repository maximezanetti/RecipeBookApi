using RecipeBookApi.Dto;
using RecipeBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Helper
{
    /// <summary>
    /// DTO Helper methods
    /// </summary>
    public class DtoHelper
    {
        public static List<IngredientDto> ConvertListIngredientToDto(List<Ingredient> ingredients)
        {
            var listIngredientsDto = new List<IngredientDto>();

            foreach (var ingredient in ingredients)
            {
                listIngredientsDto.Add(new IngredientDto
                {
                    IngredientAmount = ingredient.IngredientAmount,
                    IngredientDescription = ingredient.IngredientDescription,
                    IngredientId = ingredient.IngredientId,
                    IngredientMeasurementType = ingredient.IngredientMeasurementType,
                    IngredientName = ingredient.IngredientName
                });
            }

            return listIngredientsDto;
        }


        /// <summary>
        /// Get Recipe DTO from Entity Recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="ingredients"></param>
        /// <param name="recipeIngredient"></param>
        /// <returns></returns>
        public static RecipeDto GetRecipeDto(Recipe recipe, List<IngredientDto> ingredients, List<RecipeIngredients> recipeIngredient)
        {
            try
            {
                var listRecipeIngredientId = recipeIngredient.Where(ri => ri.RecipeId == recipe.RecipeId).Select(r => r.IngredientId);

                var ingredientForRecipe = ingredients.Where(i => listRecipeIngredientId.Contains(i.IngredientId)).ToList();

                var newRecipeDto = new RecipeDto
                {
                    Description = recipe.Description,
                    Title = recipe.Title,
                    PreparationTime = recipe.PreparationTime,
                    Instructions = recipe.Instructions,
                    NumberOfPeople = recipe.NumberOfPeople,
                    Image = recipe.Image,
                    Ingredients = ingredientForRecipe

                };

                if (recipe.RecipeId > 0)
                {
                    newRecipeDto.RecipeId = recipe.RecipeId;
                }

                return newRecipeDto;

            }
            catch (Exception ex)
            {
                // TODO: Log error.
                throw;
            }
        }

        public static RecipeDto ConvertRecipeToDto(Recipe recipe)
        {
            return new RecipeDto
            {
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                NumberOfPeople = recipe.NumberOfPeople,
                PreparationTime = recipe.PreparationTime,
                RecipeId = recipe.RecipeId,
                Title = recipe.Title
            };
        }
    }
}
