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
using ReceiptsWebMVCApp.Extensions;

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
            var product = this.db.Products.OrderBy(x => x.ProductName).Select(ProductViewModel.ViewModel).ToList();
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
                //TODO: Ако модела не е валиден да се даде възможност на юзъра да поправи модела !!!!
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
        [HttpGet]
        public ActionResult Edit(int id, string foodType, string oldFoodType)
        {

            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var model = new ReceiptsInputModel();
            if (foodType == "Предястие")
            {
                var recipeToEdit = this.db.Salads.Where(s => s.ID == id).FirstOrDefault(s => s.AuthorId == currentUserId || isAdmin);
                var productsTags = this.db.SaladProductTags.Where(p => p.SaladID == id).ToList();
                var products = this.db.Products.Select(ProductViewModel.ViewModel).ToList();
                foreach (var product in productsTags)
                {
                    foreach (var pr in products)
                    {
                        if (product.ID == pr.Id)
                        {
                            pr.IsSelected = true;
                        }
                    }
                }
                if (recipeToEdit == null)
                {
                    this.AddNotification("Промяната е невъзможна!", NotificationType.ERROR);

                    return Redirect("~/Home/Index");
                }
                else
                {
                    model = ReceiptsInputModel.CreateFromSalad(recipeToEdit, products);
                    
                }
            }

            else if (foodType == "Основно ястие")
            {
                var recipeToEdit = this.db.Meals.Where(m => m.ID == id).FirstOrDefault(s => s.AuthorId == currentUserId || isAdmin);
                var productsTags = this.db.MealProductTags.Where(p => p.MealID == id).ToList();
                var products = this.db.Products.Select(ProductViewModel.ViewModel).ToList();
                foreach (var product in productsTags)
                {
                    foreach (var pr in products)
                    {
                        if (product.ID == pr.Id)
                        {
                            pr.IsSelected = true;
                        }
                    }
                }
                if (recipeToEdit == null)
                {
                    this.AddNotification("Промяната е невъзможна!", NotificationType.ERROR);

                    return Redirect("~/Home/Index");
                }
                else
                {
                    model = ReceiptsInputModel.CreateFromMeal(recipeToEdit, products);
                    
                }

            }

            else if(foodType == "Десерт")
            {
                var recipeToEdit = this.db.Desserts.Where(d => d.ID == id).FirstOrDefault(s => s.AuthorId == currentUserId || isAdmin);
                var productsTags = this.db.DessertProductTags.Where(p => p.DessertID == id).ToList();
                var products = this.db.Products.Select(ProductViewModel.ViewModel).ToList();
                foreach (var product in productsTags)
                {
                    foreach (var pr in products)
                    {
                        if (product.ID == pr.Id)
                        {
                            pr.IsSelected = true;
                        }
                    }
                }
                if (recipeToEdit == null)
                {
                    this.AddNotification("Промяната е невъзможна!", NotificationType.ERROR);

                    return Redirect("~/Home/Index");
                }
                else
                {
                    model = ReceiptsInputModel.CreateFromDessert(recipeToEdit, products);
                    
                }

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id, ReceiptsInputModel model, string food)
        {
            if (food != model.FoodType)
            {
                if (food == "Предястие")
                {
                    var itemToDelete = this.db.Salads.Find(id);
                    var b = this.db.SaladProductTags.Where(s => s.SaladID == id).ToList();     
                    foreach (var item in b)
                    {
                        this.db.SaladProductTags.Remove(item);
                    }

                    this.db.Salads.Remove(itemToDelete);

                }
                else if (food == "Основно ястие")
                {
                    var itemToDelete = this.db.Meals.Find(id);
                    var b = this.db.MealProductTags.Where(s => s.MealID == id).ToList();
                    foreach (var item in b)
                    {
                        this.db.MealProductTags.Remove(item);
                    }
                    this.db.Meals.Remove(itemToDelete);
                }
                else if (food == "Десерт")
                {
                    var itemToDelete = this.db.Desserts.Find(id);
                    var b = this.db.DessertProductTags.Where(s => s.DessertID == id).ToList();
                    foreach (var item in b)
                    {
                        this.db.DessertProductTags.Remove(item);
                    }
                    
                    this.db.Desserts.Remove(itemToDelete);
                   
                }
                this.db.SaveChanges();
                Create(model);

            }
            else
            {
                if (model.FoodType == "Предястие")
                {
                    var e = this.db.Salads.Find(id);
                    var b = this.db.SaladProductTags.Where(s => s.SaladID == id).ToList();
                    
                    e.Title = model.Title;
                    e.Description = model.Description;
                    e.Date = DateTime.Now;
                    for (int i = 0; i < Math.Max(b.Count(), model.Products.Count()); i++)
                    {
                        if (i < b.Count)
                        {
                            this.db.SaladProductTags.Remove(b[i]);
                        }
                        if (model.Products[i].IsSelected)
                        {
                            var t = new SaladProductTags()
                            {
                                ProductID = model.Products[i].Id,
                                SaladID = id
                            };
                            this.db.SaladProductTags.Add(t);
                        }
                    }
                    this.db.SaveChanges();
                    

                }


                else if (model.FoodType == "Основно ястие")
                {
                    var e = this.db.Meals.Find(id);
                    var b = this.db.MealProductTags.Where(s => s.MealID == id).ToList();

                    e.Title = model.Title;
                    e.Description = model.Description;
                    e.Date = DateTime.Now;
                    for (int i = 0; i < Math.Max(b.Count(), model.Products.Count()); i++)
                    {
                        if (i < b.Count)
                        {
                            this.db.MealProductTags.Remove(b[i]);
                        }
                        if (model.Products[i].IsSelected)
                        {
                            var t = new MealProductTags()
                            {
                                ProductID = model.Products[i].Id,
                                MealID = id
                            };
                            this.db.MealProductTags.Add(t);
                        }
                    }
                    this.db.SaveChanges();
                }
                else if (model.FoodType == "Десерт")
                {
                    var e = this.db.Desserts.Find(id);
                    var b = this.db.DessertProductTags.Where(s => s.DessertID == id).ToList();

                    e.Title = model.Title;
                    e.Description = model.Description;
                    e.Date = DateTime.Now;
                    for (int i = 0; i < Math.Max(b.Count(), model.Products.Count()); i++)
                    {
                        if (i < b.Count)
                        {
                            this.db.DessertProductTags.Remove(b[i]);
                        }
                        
                        if (model.Products[i].IsSelected)
                        {
                            var t = new DessertProductTags()
                            {
                                ProductID = model.Products[i].Id,
                                DessertID = id
                            };
                            this.db.DessertProductTags.Add(t);
                        }
                    }
                    this.db.SaveChanges();
                }
            }
           
            return Redirect("~/Receipt/Index");
        }
        
        public ActionResult Delete(int id, string foodType)
        {
            if (foodType == "Предястие")
            {
                var e = this.db.Salads.Find(id);
                var b = this.db.SaladProductTags.Where(s => s.SaladID == id).ToList();
                this.db.Salads.Remove(e);
                foreach (var item in b)
                {
                    this.db.SaladProductTags.Remove(item);
                }
                this.db.SaveChanges();
            }

            if (foodType == "Основно ястие")
            {
                var e = this.db.Meals.Find(id);
                var b = this.db.MealProductTags.Where(s => s.MealID == id).ToList();
                this.db.Meals.Remove(e);
                foreach (var item in b)
                {
                    this.db.MealProductTags.Remove(item);
                }
                this.db.SaveChanges();
            }

            if (foodType == "Десерт")
            {
                var e = this.db.Desserts.Find(id);
                var b = this.db.DessertProductTags.Where(s => s.DessertID == id).ToList();
                this.db.Desserts.Remove(e);
                foreach (var item in b)
                {
                    this.db.DessertProductTags.Remove(item);
                }
                this.db.SaveChanges();
            }
            return Redirect("~/Home/Index");
        }
    }
}