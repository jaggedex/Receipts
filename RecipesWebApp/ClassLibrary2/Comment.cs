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
        [Key]
        public int ID { get; set; }

        public string Text { get; set; }

        public ApplicationUser Author { get; set; }

    }
}
