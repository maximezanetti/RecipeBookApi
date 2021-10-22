using RecipeBookApi.Dto;
using RecipeBookApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Helper
{
    public class ServiceResponseHelper
    {
        public static RecipeDto ConvertServiceResponseDataToRecipeDto(ServiceResponse<RecipeDto> serviceResponse)
        {
            return (RecipeDto)serviceResponse.Data;
        }
        public static  IngredientDto ConvertServiceResponseDataToIngredientDto(ServiceResponse<IngredientDto> serviceResponse)
        {
            return (IngredientDto)serviceResponse.Data;
        }
    }
}
