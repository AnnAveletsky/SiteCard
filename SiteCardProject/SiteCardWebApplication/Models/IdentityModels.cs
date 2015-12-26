using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SiteCardWebApplication.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin1 = new ApplicationUser { Email = "annserba94@gmail.com", UserName = "annserba94@gmail.com" };
            string password1 = "8255Ann_";
            var result1 = userManager.Create(admin1, password1);
            var admin2 = new ApplicationUser { Email = "ixus.van.axel@gmail.com", UserName = "ixus.van.axel@gmail.com" };
            string password2 = "8255Ann_";
            var result2 = userManager.Create(admin2, password2);

            // если создание пользователя прошло успешно
            if (result1.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin1.Id, role1.Name);
                userManager.AddToRole(admin1.Id, role2.Name);
            }
            if (result2.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin2.Id, role1.Name);
                userManager.AddToRole(admin2.Id, role2.Name);
            }

            base.Seed(context);
        }
    }

}