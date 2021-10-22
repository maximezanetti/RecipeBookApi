using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeBookApi.Dto
{
    /// <summary>
    /// Ingredient DTO
    /// </summary>
    public class IngredientDto
    {
        public long IngredientId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The ingredient name is too long. Enter less than {1} characters")]
        public string IngredientName { get; set; }


        [Required]
        [MaxLength(100, ErrorMessage = "The ingredient Description is too long. Enter less than {1} characters")]
        public string IngredientDescription { get; set; }


        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Please enter a value up to two decimals places.")]
        public double IngredientAmount { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "The ingredient Description is too long. Enter less than {1} characters")]
        public string IngredientMeasurementType { get; set; }

    }
}
