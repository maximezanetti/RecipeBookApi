using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Dto;
using RecipeBookApi.Helper;
using RecipeBookApi.Models;
using RecipeBookApi.Services;

namespace RecipeBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeIngredientService _recipeIngredientService;
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;

        public RecipesController(IRecipeIngredientService recipeIngredientService, IRecipeService recipeService, IIngredientService ingredientService)
        {
            _recipeIngredientService = recipeIngredientService;
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var serviceResponse = new ServiceResponse<RecipeDto>();
            var listRecipes = new List<RecipeDto>();

            try
            {
                serviceResponse = await _recipeService.GetRecipes();

                if (serviceResponse.Success)
                {
                    listRecipes = (List<RecipeDto>)serviceResponse.Data;
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                //TODO: Log exception
            }

            return Ok(listRecipes);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(long id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var recipeDto = new RecipeDto();

            try
            {
                var serviceResponse = await _recipeService.GetRecipe(id);

                if (serviceResponse.Success)
                {
                    recipeDto = ServiceResponseHelper.ConvertServiceResponseDataToRecipeDto(serviceResponse);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // TODO: Log Exception.
            }

            return Ok(recipeDto);
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(long id, RecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            RecipeDto updatedRecipeDto;

            try
            {
                var recipeDtoDb = await _recipeService.GetRecipe(id);
                await _recipeIngredientService.DeleteByRecipeId(id);

                var serviceResponse = _recipeService.UpdateRecipe(recipeDto);
                updatedRecipeDto = ServiceResponseHelper.ConvertServiceResponseDataToRecipeDto(serviceResponse);

                if(updatedRecipeDto == null)
                {
                    return NotFound();
                }

                foreach (var ingredient in updatedRecipeDto.Ingredients)
                {
                    var addedIngredientResponse = await _ingredientService.AddIngredient(ingredient);
                    var ingredientDto = (IngredientDto)addedIngredientResponse.Data;
                    await _recipeIngredientService.AddRecipeIngredient(new RecipeIngredientDto
                    {
                        IngredientId = ingredientDto.IngredientId,
                        RecipeId = recipeDto.RecipeId
                    });
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRecipe", new { id = updatedRecipeDto.RecipeId }, updatedRecipeDto);
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(RecipeDto recipe)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var addedRecipeDto = new RecipeDto();

            try
            {
                // Validation: At least 3 ingredients included in the recipe.
                if (recipe != null && recipe.Ingredients == null || (recipe != null && recipe.Ingredients.Count < 3))
                {
                    var errors = new Dictionary<string, string[]>
                {
                    { "Ingredients", new string[] { "At least 3 ingredients need to be added." } }
                };

                    return ValidationProblem(new ValidationProblemDetails(errors));
                }
                // TODO: add validation for ingredients.
                //if(recipe.Ingredients.Any(i => i.))

                var addedRecipeResponse = await _recipeService.AddRecipe(recipe);
                addedRecipeDto = ServiceResponseHelper.ConvertServiceResponseDataToRecipeDto(addedRecipeResponse);

                foreach (var ingredient in addedRecipeDto.Ingredients)
                {
                    var addedIngredientResponse = await _ingredientService.AddIngredient(ingredient);
                    var addedIngredientDto = ServiceResponseHelper.ConvertServiceResponseDataToIngredientDto(addedIngredientResponse);
                    await _recipeIngredientService.AddRecipeIngredient(new RecipeIngredientDto
                    {
                        IngredientId = addedIngredientDto.IngredientId,
                        RecipeId = addedRecipeDto.RecipeId
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, addedRecipeDto);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(long id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            try
            {
                var recipe = await _recipeService.GetRecipe(id);
                if (recipe.Data == null)
                {
                    return NotFound();
                }

                var toBeDeleted = _recipeService.DeleteRecipe(id);
            }
            catch (Exception ex)
            {
                // TODO: Log Exception
            }

            return NoContent();
        }

        private bool RecipeExists(long id)
        {
            var recipe = _recipeService.GetRecipe(id);
            return recipe != null && recipe.Result != null && recipe.Result.Data != null;
        }
    }
}
