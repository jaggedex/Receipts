using Microsoft.Ajax.Utilities;
using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ReceiptsWebMVCApp.Models
{
    public class ReceiptsViewModels
    {
        [UIHint("ReceiptsView")]
        public IEnumerable<FoodViewModels> Deserts { get; set; }
        [UIHint("ReceiptsView")]
        public IEnumerable<FoodViewModels> Meals { get; set; }
        [UIHint("ReceiptsView")]
        public IEnumerable<FoodViewModels> Salads { get; set; }
    }
}