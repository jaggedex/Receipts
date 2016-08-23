using RecipeWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWebData
{
    public class Product
    {
        public Product()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        [Key]
        public int ID { get; set; }

        public string ProductName { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<RecipeConfirm> RecipesConfirm { get; set; }


    }
}
