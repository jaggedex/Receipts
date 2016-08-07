using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReceiptsWebMVCData
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IDbSet<Salad> Salads { get; set; }

        public IDbSet<Meal> Meals { get; set; }

        public IDbSet<Dessert> Desserts { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<SaladProductTags> SaladProductTags { get; set; }

        public IDbSet<MealProductTags> MealProductTags { get; set; }

        public IDbSet<DessertProductTags> DessertProductTags { get; set; }

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