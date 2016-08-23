
using RecipeWebData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RecipesWebApp.Models
{
    public class RecipeViewModel
    {
        public int ID { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }
        
        public string AuthorId { get; set; }


        public virtual List<SelectListItem> ProductsConfirm { get; set; }

        public virtual List<SelectListItem> Products { get; set; }
        public static Expression<Func<RecipeConfirm, RecipeViewModel>> ViewModel
        {
            get
            {
                return e => new RecipeViewModel()
                {
                    ID = e.ID,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Type = e.Type,

                };
            }
        }
    }
}