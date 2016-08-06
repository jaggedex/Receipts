using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptsWebMVCData
{
    public class Salad
    {
        public Salad()
        {
            this.Date = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public int Rating { get; set; }

        public IEnumerable<Comment> Comments
        {
            get; set;
        }
        }
}
