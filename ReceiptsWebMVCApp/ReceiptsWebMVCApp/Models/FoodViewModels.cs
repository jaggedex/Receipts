using ReceiptsWebMVCData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ReceiptsWebMVCApp.Models
{
    
    public class FoodViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime Date { get; set; }
        
        public static Expression<Func<Salad, FoodViewModels>> Salad
        {
            get
            {
                return e => new FoodViewModels()
                {
                    Id = e.ID,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Author = e.Author
                };
            }
        }
      
        public static Expression<Func<Dessert, FoodViewModels>> Dessert
        {
            get
            {
                return e => new FoodViewModels()
                {
                    Id = e.ID,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Author = e.Author
                };
            }
        }
      
        public static Expression<Func<Meal, FoodViewModels>> Meal
        {
            get
            {
                return e => new FoodViewModels()
                {
                    Id = e.ID,
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Author = e.Author
                };
            }
        }
    }
}