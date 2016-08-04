using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptsWebMVCData
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
