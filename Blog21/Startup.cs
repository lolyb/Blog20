using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Blog21.Data;
using System.Collections.Generic;
using Blog21.Data.Repository;
using Blog21.Models;
//using Blog21.Services.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Blog21.Data.FileManager;
using System.Linq;

namespace Blog21
{
    public class Startup
    {
        // fields (properties)
        private IConfiguration _config;


        // constructor
        public Startup(IConfiguration config){
            _config = config;    
        }
            
 

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer(_config["DefaultConnection"]));

            services.AddIdentity<IdentityUser,IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })

                 // .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>{
                     options.LoginPath = "/Auth/Login";
            });

            services.AddTransient<IRepository, Repository>();
            services.AddMvc();
               
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
        }
    }
}

