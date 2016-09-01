using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesWebData
{
    public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string AuthorName { get; set; }
        public string AuthorId { get; set; }

        public virtual Recipe Recipes { get; set; }
    }
}
