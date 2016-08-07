using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptsWebMVCData
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        public string ProductName { get; set; }
        
    }
}
