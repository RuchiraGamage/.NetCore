﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CakeShop
{
    public class Startup
    {

        private IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
               // .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json",true)
                .Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        //register those services
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbcontext>(options =>
                    options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            //here if we want to add more databases for store information like login information in seperate DB
            //we can register them as above and can also use those seperate DB's to store identity details by
            //parsing those context to the below ".AddEntityFrameworkStores<AppDbcontext>();" parameter

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbcontext>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPieRepository, PieRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc();

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //middleware
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/AppException");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:"categoryFilter",
                    template:"Pie/{action}/{category?}",
                    defaults:new {controller="Pie",action="List"}
                    );

            routes.MapRoute(
                name: "default",    //route name
                template: "{controller=Home}/{action=Index}/{id?}"     //pattern
                //  defaults:new {controller="Home",action="Index"}
                );

            });

          // app.UseMvcWithDefaultRoute();//{controller=Home}/{action=index}/{id?}

          //  We recommend that you use the Configure method only to set up the request pipeline.Application startup code belongs in the Main method

            DbInitializer.Seed(app);
        }
    }
}
