using RecipesWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Models
{
    public class RecipeInputViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title can't be empty!")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }


        //[Required(ErrorMessage = "Products can't be empty!")]
        [Display(Name = "Необходими продукти")]
        public virtual List<SelectListItem> Products { get; set; }

        [Required(ErrorMessage = "Description can't be empty!")]
        [Display(Name = "Начин на приготвяне")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This can't be empty!")]
        [Display(Name = "Категория")]
        public string Type { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Оценка")]
        public int Rating { get; set; }

        public bool IsVoted { get; set; }


        public static RecipeInputViewModel CreateFromRecipe(Recipe s, List<SelectListItem> p)
        {
            return new RecipeInputViewModel()
            {
                Id = s.ID,
                Title = s.Title,
                Products = p,
                Description = s.Description,
                Date = s.Date,
                Type = s.Type



            };
        }

    }
}