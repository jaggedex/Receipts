using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipesWebApp.Models;
using System.Collections;
using RecipesWebData;
using Microsoft.AspNet.Identity;
using System.Net;
using RecipesWebApp.Extensions;
using RecipeWebData;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace RecipesWebApp.Controllers
{
    public class RecipeController : BaseController
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAppetizers(int? page)
        {
            var appetizers = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Предястие").OrderByDescending(x => x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(appetizers);

        }

        public ActionResult ListMainDishes(int? page)
        {
            var mainDishes = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Основно ястие").OrderByDescending(x => x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(mainDishes);
        }

        public ActionResult ListDesserts(int? page)
        {
            var desserts = this.db.Recipes.Include(r => r.Author).Where(a => a.Type == "Десерт").OrderByDescending(x => x.Date).ToList().ToPagedList(page ?? 1, 5);
            return View(desserts);
        }
        [Authorize]
        public ActionResult Create()
        {
            var products = this.db.Products.OrderBy(x => x.ProductName).Select(p => new SelectListItem()
            {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();


            return View(new RecipeInputViewModel { SelectProducts = products });
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeInputViewModel model, HttpPostedFileBase ChoosenFile)
        {
            if (model != null && this.ModelState.IsValid)
            {

                if (ChoosenFile != null)
                {
                    model.Image = new byte[ChoosenFile.ContentLength];
                    ChoosenFile.InputStream.Read(model.Image, 0, ChoosenFile.ContentLength);
                }
                var newProductsName = new List<string>();
                var newConfirmProduct = new List<ProductsConfirm>();
                if (model.newProduct != null)
                {

                    newProductsName = model.newProduct.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var pr in newProductsName)
                    {
                        var prod = new ProductsConfirm() { ProductName = pr };
                        this.db.ProductsConfirm.Add(prod);

                    }
                    this.db.SaveChanges();

                    foreach (var pr in newProductsName)
                    {
                        var selectedNewProducts = this.db.ProductsConfirm.Where(x => x.ProductName == pr).FirstOrDefault();
                        newConfirmProduct.Add(selectedNewProducts);
                    }

                }
                var selectedProducts = new List<Product>();
                foreach (var product in model.SelectProducts)
                {
                    if (product.Selected && product.Value != null && product.Text != null)
                    {
                        var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
                        selectedProducts.Add(pr);
                    }
                }
                // TODO ДА СЕ СЪЗДАВА В МЕЖДИННА ТАБЛИЦА ОТ КОЯТО АДМИНА ДА ГИ ПРЕХВЪРЛЯ !!!!
                //var unencoded = model.Description;
                //var encoded = HttpUtility.HtmlEncode(unencoded);
                
                var newRecipe = new RecipeConfirm()
                
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    Description = model.Description,
                    Type = model.Type,
                    Products = selectedProducts,
                    ProductsConfirm = newConfirmProduct,
                    Image = model.Image
                };
                this.db.RecipesConfirm.Add(newRecipe);
                this.db.SaveChanges();
                this.AddNotification("Рецептата предстои да бъде одобрена от администратор.", NotificationType.SUCCESS);
                switch (model.Type)
                {
                    case "Предястие":
                        return Redirect("~/Recipe/ListAppetizers");
                    case "Основно ястие":
                        return Redirect("~/Recipe/ListMainDishes");
                    case "Десерт":
                        return Redirect("~/Recipe/ListDesserts");
                }
            }
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var recipe = this.db.Recipes.Include(p => p.Author).Where(x => x.ID == id).Select(RecipeInputViewModel.ViewModel).FirstOrDefault();
            recipe.CurrentUserId = this.User.Identity.GetUserId();
            return View(recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(RecipeInputViewModel model)
        {
            var rating = new Rating()
            {
                Vote = model.Rating,
                AuthorId = this.User.Identity.GetUserId()
            };
            this.db.SaveChanges();

            var recipe = this.db.Recipes.Find(model.Id);

            recipe.Ratings.Add(rating);
            this.db.SaveChanges();
            this.AddNotification("Вие оценихте рецептата успешно.", NotificationType.SUCCESS);
            return Redirect("/Recipe/Details/" + model.Id);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var recipeToDelete = this.db.Recipes.Find(id);
            recipeToDelete.Comments.Clear();
            this.db.SaveChanges();
            if (recipeToDelete == null)
            {
                return HttpNotFound();
            }

            if (recipeToDelete.AuthorId == User.Identity.GetUserId() || IsAdmin())
            {
                this.db.Recipes.Remove(recipeToDelete);
                this.db.SaveChanges();
                this.AddNotification("Рецептата е премахната успешно.", NotificationType.SUCCESS);
            }
            return Redirect("/Recipe/Index");
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var recipeToEdit = this.db.Recipes.Where(recipe => recipe.ID == id).Select(RecipeInputViewModel.ViewModel).FirstOrDefault();

            var products = this.db.Products.OrderBy(x => x.ProductName).Select(p => new SelectListItem()
            {
                Text = p.ProductName,
                Value = p.ID.ToString(),
                Selected = false
            }).ToList();

            foreach (var product in recipeToEdit.Products)
            {
                foreach (var pr in products)
                {
                    if (product.ID == int.Parse(pr.Value))
                    {
                        pr.Selected = true;
                    }
                }
            }
            recipeToEdit.SelectProducts = products;
            if (this.User.Identity.GetUserId() == recipeToEdit.AuthorId || IsAdmin())
            {
                return View(recipeToEdit);
            }
            else
            {
                this.AddNotification("Нямате право да променяте тази рецепта.", NotificationType.WARNING);
                return Redirect("/Recipe/Index");


            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecipeInputViewModel model, HttpPostedFileBase ChoosenFile)
        {
            var editedRecipe = this.db.Recipes.Find(model.Id);

            if (model != null && this.ModelState.IsValid)
            {
                if (ChoosenFile != null)
                {
                    model.Image = new byte[ChoosenFile.ContentLength];
                    ChoosenFile.InputStream.Read(model.Image, 0, ChoosenFile.ContentLength);
                }
                var newProductsName = new List<string>();
                var newConfirmProduct = new List<ProductsConfirm>();
                if (model.newProduct != null)
                {

                    newProductsName = model.newProduct.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (var pr in newProductsName)
                    {
                        var prod = new ProductsConfirm() { ProductName = pr };
                        this.db.ProductsConfirm.Add(prod);

                    }
                    this.db.SaveChanges();

                    foreach (var pr in newProductsName)
                    {
                        var selectedNewProducts = this.db.ProductsConfirm.Where(x => x.ProductName == pr).FirstOrDefault();
                        newConfirmProduct.Add(selectedNewProducts);
                    }

                }
                var selectedProducts = new List<Product>();
                foreach (var product in model.SelectProducts)
                {
                    if (product.Selected && product.Value != null && product.Text != null)
                    {
                        var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
                        selectedProducts.Add(pr);
                    }
                }
       
                var newRecipe = new RecipeConfirm()

                {
                    AuthorId = editedRecipe.AuthorId,
                    Title = model.Title,
                    Description = model.Description,
                    Type = model.Type,
                    Products = selectedProducts,
                    ProductsConfirm = newConfirmProduct,
                    Image = model.Image
                };
                this.db.RecipesConfirm.Add(newRecipe);
                this.db.SaveChanges();
                editedRecipe.Comments.Clear(); //new
                this.db.Recipes.Remove(editedRecipe); //new
                this.db.SaveChanges(); //new
                

                this.AddNotification("Рецептата предстои да бъде одобрена от администратор.", NotificationType.SUCCESS);
                switch (model.Type)
                {
                    case "Предястие":
                        return Redirect("~/Recipe/ListAppetizers");
                    case "Основно ястие":
                        return Redirect("~/Recipe/ListMainDishes");
                    case "Десерт":
                        return Redirect("~/Recipe/ListDesserts");
                }
            }
            //var selectedProducts = new List<Product>();
            //foreach (var product in model.SelectProducts)
            //{
            //    if (product.Selected && product.Value != null && product.Text != null)
            //    {
            //        var pr = this.db.Products.Where(prd => prd.ID.ToString() == product.Value).FirstOrDefault();
            //        selectedProducts.Add(pr);
            //    }
            //}
            //editedRecipe.Products.Clear();
            //editedRecipe.Title = model.Title;
            //editedRecipe.Description = model.Description;
            //editedRecipe.Type = model.Type;
            //editedRecipe.Products = selectedProducts;
            //editedRecipe.Date = DateTime.Now;
            //editedRecipe.AuthorId = this.User.Identity.GetUserId();


            

            return Redirect("/Recipe/Details/" + model.Id);
        }
        [Authorize]
        public ActionResult AddComment()
        {
            return PartialView("_ShowComments");
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddComment(string CommentText, int id)
        {
            if (CommentText != "")
            {
            var newComment = new Comment()
            {
                Text = CommentText,
                AuthorId = this.User.Identity.GetUserId(),
                AuthorName = this.User.Identity.GetUserName()
            };
            this.db.SaveChanges();
            var recipeToAddComment = this.db.Recipes.Find(id);
            recipeToAddComment.Comments.Add(newComment);
            this.AddNotification("Коментарът ви е добавен успешно.", NotificationType.SUCCESS);
            this.db.SaveChanges();
            }
            else
            {
                this.AddNotification("Не можете да добавите празен коментар.", NotificationType.WARNING);
            }
            return Redirect("/Recipe/Details/" + id);
        }

        public ActionResult CommentToDelete(int id, int recipeId)
        {
            var commentToDelete = this.db.Comments.Find(id);
            this.db.Comments.Remove(commentToDelete);
            this.db.SaveChanges();
            this.AddNotification("Коментарът ви е добавен успешно.", NotificationType.INFO);
           
            return Redirect("/Recipe/Details/" + recipeId);
        }

        public ActionResult CommentToEdit(int id, int recipeId)
        {
            var commentToEdit = this.db.Comments.Find(id);
            return PartialView("_CommentToEdit" , commentToEdit);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CommentToEdit(Comment CommentToEdit, int Id, int recipeId)
        {
            var commentToEdit = this.db.Comments.Find(Id);
            commentToEdit.Text = CommentToEdit.Text;
            this.db.SaveChanges();
            this.AddNotification("Коментарът ви е променен успешно.", NotificationType.INFO);
            return Redirect("/Recipe/Details/" + recipeId);
        }
        public ActionResult My(int? page)
        {
            var currentUser = this.User.Identity.GetUserId();
            var myRecipes = this.db.Recipes.Include(a => a.Author).Where(x => x.AuthorId == currentUser).ToList();
            var pagedMyRecipes = myRecipes.ToPagedList(page ?? 1, 5);
            return View(pagedMyRecipes);
        }

    }
}

