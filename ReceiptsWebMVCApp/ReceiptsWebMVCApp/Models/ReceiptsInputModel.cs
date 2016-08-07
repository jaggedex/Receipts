using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceiptsWebMVCApp.Models
{
    public class ReceiptsInputModel
    {

        [Required(ErrorMessage = "Title can't be empty!")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        
        //[Required(ErrorMessage = "Products can't be empty!")]
        [Display(Name = "Необходими продукти")]
        public virtual List<ProductViewModel> Products { get; set; }
        
        [Required(ErrorMessage = "Description can't be empty!")]
        [Display(Name = "Начин на приготвяне")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This can't be empty!")]
        [Display(Name = "Категория")]
        public string FoodType { get; set; }
        
    }
}