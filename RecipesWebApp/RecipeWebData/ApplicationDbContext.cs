using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RecipeWebData;

namespace RecipesWebData

{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Recipe> Recipes { get; set; }


        public IDbSet<Product> Products { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Rating> Ratings { get; set; }

        public IDbSet<RecipeConfirm> RecipesConfirm { get; set; }

        public IDbSet<ProductsConfirm> ProductsConfirm { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
