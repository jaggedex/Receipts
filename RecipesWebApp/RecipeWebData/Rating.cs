using RecipesWebData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeWebData
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public int Vote { get; set; }

        public string AuthorId { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
