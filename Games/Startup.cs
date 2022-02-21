using Games.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Games.Startup))]
namespace Games
{
    public partial class Startup
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            IdentityRole role = new IdentityRole();

            if(!roleManager.RoleExists("Admins"))
            {
                role.Name = "Admins";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "Ahmed";
                user.Email = "Ahmedabdelatyeelu@gmail.com";
                var check = userManager.Create(user, "123456a");

                if(check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins");
                }

            }
        }
    }
}
