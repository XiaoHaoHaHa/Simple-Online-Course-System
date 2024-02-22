
using CoreLib;
using DataBase;
using DataBase.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using CoreLib.Interface;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder.Extensions;
using FrontEndMVC.Models;

namespace FrontEndMVC
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
            //services.AddAuthentication(
            //    CertificateAuthenticationDefaults.AuthenticationScheme)
            //    .AddCertificate();
            services.AddControllersWithViews();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<IUserRegister, UserRegister>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginCheck, LoginCheck>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStuSelectRepo, StuSelectRepo>();
            services.AddScoped<IStudentSelect, StudentSelect>();
            services.AddDbContext<TainanNetContext>(options =>options.UseSqlServer("name=ConnectionStrings:TainanNetContextDB"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
                {
                    option.LoginPath = new PathString("/Login/LoginCheck");
                    option.LogoutPath = new PathString("/Login/");

                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    option.SlidingExpiration = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //app.UseExceptionHandler("/Home/Error");
            ////The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Course}/{action=Index}/{id?}");
            });
        }
    }
}
