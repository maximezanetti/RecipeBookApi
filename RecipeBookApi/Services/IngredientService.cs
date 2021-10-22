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
    public class IngredientService : IIngredientService
    {
        private readonly RecipeBookDbContext _context;

        public IngredientService(RecipeBookDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IngredientDto>> GetIngredient(long ingredientId)
        {
            var response = new ServiceResponse<IngredientDto>();

            try
            {
                Ingredient ingredient = await _context.Ingredients
                    .FirstOrDefaultAsync(c => c.IngredientId == ingredientId);

                if (ingredient == null)
                {
                    response.Success = false;
                    response.Message = "Recipe not found.";
                    return response;
                }

                response.Success = true;
                response.Message = "Ingredients inserted";
                response.Data = ingredient;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<IngredientDto>> AddIngredient(IngredientDto newIngredientDto)
        {
            var response = new ServiceResponse<IngredientDto>();

            try
            {
                var newIngredient = new Ingredient
                {
                    IngredientAmount = newIngredientDto.IngredientAmount,
                    IngredientDescription = newIngredientDto.IngredientDescription,
                    IngredientMeasurementType = newIngredientDto.IngredientMeasurementType,
                    IngredientName = newIngredientDto.IngredientName
                };

                var addedIngredient = await _context.Ingredients.AddAsync(newIngredient);
                await _context.SaveChangesAsync();

                newIngredientDto.IngredientId = addedIngredient.Entity.IngredientId;
                response.Success = true;
                response.Message = "Ingredients inserted";
                response.Data = newIngredientDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Get all ingredients from Database and convert them into Ingredient DTO
        /// </summary>
        /// <returns>IngredientDto</returns>
        public async Task<List<IngredientDto>> GetIngredients()
        {
            var listIngredientsDto = new List<IngredientDto>();

            try
            {
                var ingredients = await _context.Ingredients.ToListAsync();
                listIngredientsDto = DtoHelper.ConvertListIngredientToDto(ingredients);
            }
            catch (Exception ex)
            {
                //TODO: log error.
                throw;
            }

            return listIngredientsDto;
        }
    }
}
