using RecipeBookApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Dto
{
    /// <summary>
    /// Recipe DTO
    /// </summary>
    public class RecipeDto
    {
        public long RecipeId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Recipe title is too long. Enter less than {1} characters")]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "The Recipe Description is too long. Enter less than {1} characters")]
        public string Description { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int NumberOfPeople { get; set; }

        [Required]
        [Range(1, 360, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int PreparationTime { get; set; }

        [Required]
        public string Instructions { get; set; }


        public byte[] Image { get; set; }

        [Required]
        [MaxLength(3,ErrorMessage = "Please add at least {1} ingredients.")]
        public ICollection<IngredientDto> Ingredients { get; set; }
    }
}
