namespace RecipesWebData.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RecipesWebData;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<RecipesWebData.ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RecipesWebData.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123"/*, "System Administrator"*/);
                CreateUser(context, "masteradmin@gmail.com", "123"/*, "Master Administrator"*/);
                CreateUser(context, "pesho@gmail.com", "123"/*, "Peter Ivanov"*/);
                CreateUser(context, "merry@gmail.com", "123"/*, "Maria Petrova"*/);
                CreateUser(context, "geshu@gmail.com", "123"/*, "George Petrov"*/);

                CreateRole(context, "Administrators");
                CreateRole(context, "MasterAdministrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");
                AddUserToRole(context, "masteradmin@gmail.com", "MasterAdministrators");
                CreateProduct(context, "Чушки");
                CreateProduct(context, "Агнешко");
                CreateProduct(context, "Говеждо месо");
                CreateProduct(context, "Гъше месо");
                CreateProduct(context, "Кренвирш");
                CreateProduct(context, "Луканка");
                CreateProduct(context, "Овче месо");
                CreateProduct(context, "Пилешко месо");
                CreateProduct(context, "Пуешко месо");
                CreateProduct(context, "Салам");
                CreateProduct(context, "Свинско месо");
                CreateProduct(context, "Телешко месо");
                CreateProduct(context, "Яйца");
                CreateProduct(context, "Бял амур");
                CreateProduct(context, "Бяла риба");
                CreateProduct(context, "Калкан");
                CreateProduct(context, "Карагьоз");
                CreateProduct(context, "Кефал");
                CreateProduct(context, "Копърка");
                CreateProduct(context, "Лаврак");
                CreateProduct(context, "Лефер");
                CreateProduct(context, "Мерлуза");
                CreateProduct(context, "Омар");
                CreateProduct(context, "Паламуд");
                CreateProduct(context, "Пъстърва");
                CreateProduct(context, "Риба тон");
                CreateProduct(context, "Сардина");
                CreateProduct(context, "Сафрид");
                CreateProduct(context, "Скумрия");
                CreateProduct(context, "Сом");
                CreateProduct(context, "Сьомга");
                CreateProduct(context, "Треска");
                CreateProduct(context, "Трицона");
                CreateProduct(context, "Филе от акула");
                CreateProduct(context, "Хайвер");
                CreateProduct(context, "Хек");
                CreateProduct(context, "Херинга");
                CreateProduct(context, "Цаца");
                CreateProduct(context, "Чига");
                CreateProduct(context, "Шаран");
                CreateProduct(context, "Щука");
                CreateProduct(context, "Извара обезмаслена");
                CreateProduct(context, "Кашкавал");
                CreateProduct(context, "Кисело мляко");
                CreateProduct(context, "Краве сирене");
                CreateProduct(context, "Овче сирене");
                CreateProduct(context, "Прясно мляко");
                CreateProduct(context, "Сметана");
                CreateProduct(context, "Жито");
                CreateProduct(context, "Леща");
                CreateProduct(context, "Макарони и спагети");
                CreateProduct(context, "Овесени трици");
                CreateProduct(context, "Овесени ядки");
                CreateProduct(context, "Ориз - Бял");
                CreateProduct(context, "Ориз - Кафяв");
                CreateProduct(context, "Просо");
                CreateProduct(context, "Пшеница");
                CreateProduct(context, "Пшенични трици");
                CreateProduct(context, "Ръж");
                CreateProduct(context, "Соя");
                CreateProduct(context, "Сухари");
                CreateProduct(context, "Тутманик");
                CreateProduct(context, "Фасул");
                CreateProduct(context, "Царевица");
                CreateProduct(context, "Артишок");
                CreateProduct(context, "Аспержи");
                CreateProduct(context, "Броколи");
                CreateProduct(context, "Брюкселско зеле");
                CreateProduct(context, "Грах");
                CreateProduct(context, "Гъби");
                CreateProduct(context, "Домати");
                CreateProduct(context, "Зеле");
                CreateProduct(context, "Зеле - Китайско");
                CreateProduct(context, "Зеле - Червено");
                CreateProduct(context, "Камби");
                CreateProduct(context, "Картофи");
                CreateProduct(context, "Карфиол");
                CreateProduct(context, "Кисело зеле");
                CreateProduct(context, "Краставици");
                CreateProduct(context, "Лук зелен");
                CreateProduct(context, "Лук стар");
                CreateProduct(context, "Маруля");
                CreateProduct(context, "Моркови");
                CreateProduct(context, "Патладжан");
                CreateProduct(context, "Репички");
                CreateProduct(context, "Ряпа");
                CreateProduct(context, "Спанак");
                CreateProduct(context, "Тиква");
                CreateProduct(context, "Тиквички");
                CreateProduct(context, "Фасул зелен");
                CreateProduct(context, "Цвекло червено");
                CreateProduct(context, "Авокадо");
                CreateProduct(context, "Ананас");
                CreateProduct(context, "Бадеми печени");
                CreateProduct(context, "Бадеми сурови");
                CreateProduct(context, "Банани");
                CreateProduct(context, "Вишни");
                CreateProduct(context, "Грейпфрут");
                CreateProduct(context, "Грозде - бяло");
                CreateProduct(context, "Грозде - розово");
                CreateProduct(context, "Грозде - червено");
                CreateProduct(context, "Диня");
                CreateProduct(context, "Дюли");
                CreateProduct(context, "Кайсии");
                CreateProduct(context, "Киви");
                CreateProduct(context, "Круши");
                CreateProduct(context, "Круши - Азиатски");
                CreateProduct(context, "Круши - диви");
                CreateProduct(context, "Ленено семе");
                CreateProduct(context, "Лешници");
                CreateProduct(context, "Лимони");
                CreateProduct(context, "Манго");
                CreateProduct(context, "Мандарини");
                CreateProduct(context, "Мармалади");
                CreateProduct(context, "Нар");
                CreateProduct(context, "Орехи");
                CreateProduct(context, "Помело");
                CreateProduct(context, "Портокали");
                CreateProduct(context, "Праскови");
                CreateProduct(context, "Пъпеш");
                CreateProduct(context, "Сливи сини");
                CreateProduct(context, "Слънчоглед");
                CreateProduct(context, "Смокини");
                CreateProduct(context, "Стафиди");
                CreateProduct(context, "Сушени кайсии");
                CreateProduct(context, "Сушени сливи");
                CreateProduct(context, "Сушени смокини");
                CreateProduct(context, "Тиква");
                CreateProduct(context, "Тиквени семки");
                CreateProduct(context, "Фурми");
                CreateProduct(context, "Фъстъци");
                CreateProduct(context, "Череши");
                CreateProduct(context, "Ябалки");
                CreateProduct(context, "Ягоди");
                CreateProduct(context, "Захар");
                CreateProduct(context, "Зехтин");
                CreateProduct(context, "Краве масло");
                CreateProduct(context, "Кълнове от пшеница");
                CreateProduct(context, "Маргарин");
                CreateProduct(context, "Маслини - зелени");
                CreateProduct(context, "Маслини - зрели");
                CreateProduct(context, "Маслини - Класик");
                CreateProduct(context, "Масло от Авокадо");
                CreateProduct(context, "Масло от Бадем");
                CreateProduct(context, "Масло от Какао");
                CreateProduct(context, "Масло от Кокос");
                CreateProduct(context, "Масло от Овес");
                CreateProduct(context, "Масло от Сусам");
                CreateProduct(context, "Мед");
                CreateProduct(context, "Олио - Слънчогледово");
                CreateProduct(context, "Олио - Соево");
                context.SaveChanges();
            }
        }


        private void CreateProduct(ApplicationDbContext context, string productName)
        {
            var product = new Product()
            {
                ProductName = productName
            };
            context.Products.Add(product);
        }
        private void CreateUser(ApplicationDbContext context,
            string email, string password/*, string fullName*/)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                //FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
    }
}

