using RecipesWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipesWebApp.Models
{
    public class SearchViewModel
    {
        [UIHint("_RecipeView")]
        public IEnumerable<Recipe> MatchedByAll { get; set; }
        [UIHint("_RecipeView")]
        public IEnumerable<Recipe> MatchedWithoutOne { get; set; }

        [UIHint("_RecipeView")]
        public IEnumerable<Recipe> MatchedByOneProduct { get; set; }
    }
}