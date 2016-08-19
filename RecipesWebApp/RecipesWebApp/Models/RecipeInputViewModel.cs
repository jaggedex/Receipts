using RecipesWebData;
using RecipeWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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

        public string AuthorId { get; set; }
        [Display(Name = "Автор")]
        public string User { get; set; }

        //[Required(ErrorMessage = "Products can't be empty!")]
        [Display(Name = "Необходими продукти")]
        public  List<SelectListItem> SelectProducts { get; set; }
        [Display(Name = "Необходими продукти")]
        public  ICollection<Product> Products { get; set; }

        [Required(ErrorMessage = "Description can't be empty!")]
        [Display(Name = "Начин на приготвяне")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This can't be empty!")]
        [Display(Name = "Категория")]
        public string Type { get; set; }

        public DateTime Date { get; set; }
        
        [Display(Name = "Оценка")]
        public int Rating { get; set; }

        public string CurrentUserId { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public PaginationViewModel Pagination { get; set; }

        public static Expression<Func<Recipe, RecipeInputViewModel>> ViewModel
        {
            get
            {
                return e => new RecipeInputViewModel()
                {
                    Id = e.ID,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Type = e.Type,
                    Products = e.Products,
                    AuthorId = e.AuthorId, 
                    Ratings = e.Ratings,
                    Comments = e.Comments

                };
            }
        }

    }
}