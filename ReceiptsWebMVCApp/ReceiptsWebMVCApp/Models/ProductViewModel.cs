using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ReceiptsWebMVCApp.Models
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public string ProductName { set; get; }
        public bool IsSelected { get; set; }


        public static Expression<Func<Product, ProductViewModel>> ViewModel
        {
            get
            {
                return e => new ProductViewModel()
                {
                    Id = e.ID,
                    ProductName = e.ProductName,
                };
            }
        }
    }
}