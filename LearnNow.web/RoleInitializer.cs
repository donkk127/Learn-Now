using Microsoft.AspNetCore.Identity;

namespace LearnNow.web
{
    public static class RoleInitializer
    {
        public static async Task SeedRoleAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles ={ "Admin", "Teacher", "Trainee"};

            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
