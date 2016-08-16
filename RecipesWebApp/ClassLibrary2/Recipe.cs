using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWebData
{
    public class Recipe
    {
        public Recipe()
        {
            this.Date = DateTime.Now;
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public int Rating { get; set; }


        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
