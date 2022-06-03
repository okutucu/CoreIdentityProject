using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdentityProject.CustomValidation;
using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreIdentityProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(opts => 
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
            });


            services.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = new PathString("/Home/Login");
                opts.Cookie = new CookieBuilder
                {
                    Name = "MyBlog",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest

                };
                opts.SlidingExpiration = true;
                opts.ExpireTimeSpan =TimeSpan.FromDays(7);

            });

            services.AddIdentity<AppUser,AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = " abcçdefðghýijklmnoöpqrsþtuüvwxyzABCDEFGHIÝJKLMNÖOPQRSTUVWXYZ0123456789-._?!";

                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddPasswordValidator<CustomPasswordValidator>().AddUserValidator<CustomUserValidator>().AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddMvc();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
