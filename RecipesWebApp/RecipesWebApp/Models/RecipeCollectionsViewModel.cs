using RecipesWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipesWebApp.Models
{
    public class RecipeCollectionsViewModel
    {   
        [UIHint("_IndexRecipeView")]
        public ICollection<Recipe> Appetizers { get; set; }
        [UIHint("_IndexRecipeView")]
        public ICollection<Recipe> MainDishes { get; set; }
        [UIHint("_IndexRecipeView")]
        public ICollection<Recipe> Desserts { get; set; }
    }
}