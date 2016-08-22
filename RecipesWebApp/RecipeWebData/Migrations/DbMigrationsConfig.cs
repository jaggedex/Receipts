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
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "������� ����");
                CreateProduct(context, "���� ����");
                CreateProduct(context, "��������");
                CreateProduct(context, "�������");
                CreateProduct(context, "���� ����");
                CreateProduct(context, "������� ����");
                CreateProduct(context, "������ ����");
                CreateProduct(context, "�����");
                CreateProduct(context, "������� ����");
                CreateProduct(context, "������� ����");
                CreateProduct(context, "����");
                CreateProduct(context, "��� ����");
                CreateProduct(context, "���� ����");
                CreateProduct(context, "������");
                CreateProduct(context, "��������");
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "������");
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "����");
                CreateProduct(context, "�������");
                CreateProduct(context, "��������");
                CreateProduct(context, "���� ���");
                CreateProduct(context, "�������");
                CreateProduct(context, "������");
                CreateProduct(context, "�������");
                CreateProduct(context, "���");
                CreateProduct(context, "������");
                CreateProduct(context, "������");
                CreateProduct(context, "�������");
                CreateProduct(context, "���� �� �����");
                CreateProduct(context, "������");
                CreateProduct(context, "���");
                CreateProduct(context, "�������");
                CreateProduct(context, "����");
                CreateProduct(context, "����");
                CreateProduct(context, "�����");
                CreateProduct(context, "����");
                CreateProduct(context, "������ �����������");
                CreateProduct(context, "��������");
                CreateProduct(context, "������ �����");
                CreateProduct(context, "����� ������");
                CreateProduct(context, "���� ������");
                CreateProduct(context, "������ �����");
                CreateProduct(context, "�������");
                CreateProduct(context, "����");
                CreateProduct(context, "����");
                CreateProduct(context, "�������� � �������");
                CreateProduct(context, "������� �����");
                CreateProduct(context, "������� ����");
                CreateProduct(context, "���� - ���");
                CreateProduct(context, "���� - �����");
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "�������� �����");
                CreateProduct(context, "���");
                CreateProduct(context, "���");
                CreateProduct(context, "������");
                CreateProduct(context, "��������");
                CreateProduct(context, "�����");
                CreateProduct(context, "��������");
                CreateProduct(context, "�������");
                CreateProduct(context, "�������");
                CreateProduct(context, "�������");
                CreateProduct(context, "���������� ����");
                CreateProduct(context, "����");
                CreateProduct(context, "����");
                CreateProduct(context, "������");
                CreateProduct(context, "����");
                CreateProduct(context, "���� - ��������");
                CreateProduct(context, "���� - �������");
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "�������");
                CreateProduct(context, "������ ����");
                CreateProduct(context, "����������");
                CreateProduct(context, "��� �����");
                CreateProduct(context, "��� ����");
                CreateProduct(context, "������");
                CreateProduct(context, "�������");
                CreateProduct(context, "���������");
                CreateProduct(context, "�������");
                CreateProduct(context, "����");
                CreateProduct(context, "������");
                CreateProduct(context, "�����");
                CreateProduct(context, "��������");
                CreateProduct(context, "����� �����");
                CreateProduct(context, "������ �������");
                CreateProduct(context, "�������");
                CreateProduct(context, "������");
                CreateProduct(context, "������ ������");
                CreateProduct(context, "������ ������");
                CreateProduct(context, "������");
                CreateProduct(context, "�����");
                CreateProduct(context, "���������");
                CreateProduct(context, "������ - ����");
                CreateProduct(context, "������ - ������");
                CreateProduct(context, "������ - �������");
                CreateProduct(context, "����");
                CreateProduct(context, "����");
                CreateProduct(context, "������");
                CreateProduct(context, "����");
                CreateProduct(context, "�����");
                CreateProduct(context, "����� - ��������");
                CreateProduct(context, "����� - ����");
                CreateProduct(context, "������ ����");
                CreateProduct(context, "�������");
                CreateProduct(context, "������");
                CreateProduct(context, "�����");
                CreateProduct(context, "���������");
                CreateProduct(context, "���������");
                CreateProduct(context, "���");
                CreateProduct(context, "�����");
                CreateProduct(context, "������");
                CreateProduct(context, "���������");
                CreateProduct(context, "��������");
                CreateProduct(context, "�����");
                CreateProduct(context, "����� ����");
                CreateProduct(context, "����������");
                CreateProduct(context, "�������");
                CreateProduct(context, "�������");
                CreateProduct(context, "������ ������");
                CreateProduct(context, "������ �����");
                CreateProduct(context, "������ �������");
                CreateProduct(context, "�����");
                CreateProduct(context, "������� �����");
                CreateProduct(context, "�����");
                CreateProduct(context, "�������");
                CreateProduct(context, "������");
                CreateProduct(context, "������");
                CreateProduct(context, "�����");
                CreateProduct(context, "�����");
                CreateProduct(context, "������");
                CreateProduct(context, "����� �����");
                CreateProduct(context, "������� �� �������");
                CreateProduct(context, "��������");
                CreateProduct(context, "������� - ������");
                CreateProduct(context, "������� - �����");
                CreateProduct(context, "������� - ������");
                CreateProduct(context, "����� �� �������");
                CreateProduct(context, "����� �� �����");
                CreateProduct(context, "����� �� �����");
                CreateProduct(context, "����� �� �����");
                CreateProduct(context, "����� �� ����");
                CreateProduct(context, "����� �� �����");
                CreateProduct(context, "���");
                CreateProduct(context, "���� - �������������");
                CreateProduct(context, "���� - �����");
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

