using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptsWebMVCData
{
    public class SaladProductTags
    {
        [Key]
        public int ID { get; set; }

        public int SaladID { get; set; }
        
        public int ProductID { get; set; }
    }
}
