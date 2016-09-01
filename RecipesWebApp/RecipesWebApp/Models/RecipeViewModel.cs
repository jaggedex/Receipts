
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
    public class RecipeViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Display(Name = "Начин на приготвяне")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        public string Type { get; set; }

        [Display(Name = "Добавена на")]
        public DateTime Date { get; set; }
        

        public string AuthorId { get; set; }

        [Display(Name = "Снимка")]
        public byte[] Image { get; set; }

        [Display(Name = "Новодобавени продукти")]
        public virtual List<SelectListItem> ProductsConfirm { get; set; }
        [Display(Name = "Необходими продукти")]
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
                    Image = e.Image
                };
            }
        }
    }
}