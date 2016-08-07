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
    public class SaladViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime Date { get; set; }

        public static Expression<Func<Salad, SaladViewModels>> ViewModel
        {
            get
            {
                return e => new SaladViewModels()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Author = e.Author
                };
            }
        }
    }
}