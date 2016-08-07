using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReceiptsWebMVCApp.Models;
using System.ComponentModel.DataAnnotations;
using ReceiptsWebMVCData;
using Microsoft.AspNet.Identity;

namespace ReceiptsWebMVCApp.Controllers
{
    public class ReceiptController : BaseController
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ListSalads()
        {
            var salads = this.db.Salads.Include(s => s.Author).Select(FoodViewModels.Salad);
            return View(salads);
        }

        public ActionResult ListMeals()
        {
            var meals = this.db.Meals.Include(m => m.Author).Select(FoodViewModels.Meal);
            return View(meals);
        }

        public ActionResult ListDesserts()
        {
            var desserts = this.db.Desserts.Include(d => d.Author).Select(FoodViewModels.Dessert);
            return View(desserts);
        }

        
        public ActionResult Create()
        {
            var product = this.db.Products.Select(ProductViewModel.ViewModel).ToList();
            var OrderVM = new ReceiptsInputModel();
            OrderVM.Products = product;
            return View(OrderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceiptsInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                if (model.FoodType.ToString() == "Предястие")
                {
                    var e = new Salad()
                    {
                        AuthorId = this.User.Identity.GetUserId(),
                        Title = model.Title,
                        Description = model.Description
                    };
                    this.db.Salads.Add(e);
                    this.db.SaveChanges();
                    var saladId = this.db.Salads.Where(s => s.Title == model.Title).FirstOrDefault();
                    foreach (var product in model.Products)
                    {
                        if (product.IsSelected)
                        {
                            var t = new SaladProductTags()
                            {
                                ProductID = product.Id,
                                SaladID = saladId.ID
                            };
                            this.db.SaladProductTags.Add(t);
                            this.db.SaveChanges();
                        }
                    }
                }
                else if (model.FoodType.ToString() == "Основно ястие")
                {
                    var e = new Meal()
                    {
                        AuthorId = this.User.Identity.GetUserId(),
                        Title = model.Title,
                        Description = model.Description
                    };
                    this.db.Meals.Add(e);
                    this.db.SaveChanges();
                    var mealId = this.db.Meals.Where(m => m.Title == model.Title).FirstOrDefault();
                    foreach (var product in model.Products)
                    {
                        if (product.IsSelected)
                        {
                            var t = new MealProductTags()
                            {
                                ProductID = product.Id,
                                MealID = mealId.ID
                            };
                            this.db.MealProductTags.Add(t);
                            this.db.SaveChanges();
                        }
                    }
                }
                else if (model.FoodType.ToString() == "Десерт")
                {
                    var e = new Dessert()
                    {
                        AuthorId = this.User.Identity.GetUserId(),
                        Title = model.Title,
                        Description = model.Description
                    };
                    this.db.Desserts.Add(e);
                    this.db.SaveChanges();
                    var dessertId = this.db.Desserts.Where(d => d.Title == model.Title).FirstOrDefault();
                    foreach (var product in model.Products)
                    {
                        if (product.IsSelected)
                        {
                            var t = new DessertProductTags()
                            {
                                ProductID = product.Id,
                                DessertID = dessertId.ID
                            };
                            this.db.DessertProductTags.Add(t);
                            this.db.SaveChanges();
                        }
                    }
                }

            }
            return Redirect("~/Home/Index");
        }


        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}