using Blog21.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog21
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            try
            {

                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
               
            ctx.Database.EnsureCreated();
        
            var adminRole = new IdentityRole("Admin");

              if (!ctx.Roles.Any())
              {
                // Create a role
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();

              }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    // Create admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };

                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    //add role to user
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                }


               } catch(Exception e)
                   {
                Console.WriteLine(e.Message);

                   }

            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
