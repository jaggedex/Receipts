using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWebData
{
    public class ProductsConfirm
    {
        //public ProductsConfirm()
        //{
        //    this.RecipesConfirm = new HashSet<RecipeConfirm>();
        //}
        [Key]
        public int ID { get; set; }

        public string ProductName { get; set; }

        public virtual ICollection<RecipeConfirm> RecipesConfirm { get; set; }

    }
}
