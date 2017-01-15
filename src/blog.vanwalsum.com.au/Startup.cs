using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using blog.vanwalsum.com.au.Data;
using blog.vanwalsum.com.au.Models;
using blog.vanwalsum.com.au.Services;
using blog.vanwalsum.com.au.Models.Repository;
using blog.vanwalsum.com.au.Data.Migrations;

namespace blog.vanwalsum.com.au
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddScoped<BlogRepository>();
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            scopeFactory.SeedData();
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "category",
                //    template: "Category/{id?}",
                //    defaults: new {controller = "Category", action="Detail"}
                //    );
                //routes.MapRoute(
                //    name: "blog",
                //    template: "Blog/{*slug}",
                //    defaults: new { controller = "Blog", action = "Post" }
                //    );

                routes.MapRoute(
                    name: "ViewPost", 
                    template: "Blog/{id}/{postName}",
                    defaults: new { controller = "Blog", action = "Post", id = "", postName = "" }
                    );
                routes.MapRoute(
                    name: "Category",
                    template: "Category/{id}",
                    defaults: new { controller = "Category", action = "Detail", id = "" }
                    );
                routes.MapRoute(
                    name: "Tag",
                    template: "Tag/{id}",
                    defaults: new { controller = "Tag", action = "Detail", id = "" }
                    );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
