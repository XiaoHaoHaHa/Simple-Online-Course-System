using CoreLib;
using CoreLib.Interface;
using CourseApi.ApiService;
using CourseApi.Middleware;
using DataBase;
using DataBase.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApi
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
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddScoped<ICourseRepo, CourseRepo>();
            services.AddScoped<IUserRegister, UserRegister>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginCheck, LoginCheck>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStuSelectRepo, StuSelectRepo>();
            services.AddScoped<IStudentSelect, StudentSelect>();
            services.AddScoped<JwtHelper>();
            services.AddDbContext<TainanNetContext>(options => options.UseSqlServer("name=ConnectionStrings:TainanNetContextDB"));
            services.AddJwtAuthentication(Configuration);

            // Register the Swagger services
            services.AddOpenApiDocument(config => {
                config.GenerateEnumMappingDescription = true; //redoc �����P��API�������ͤU�Կ��
                config.Version = "v1"; //�]�w API ������T
                config.Title = "My API"; //�]�w���D
                config.DocumentName = "v1"; //�]�wAPI���W��
                config.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower(),
                    Type = NSwag.OpenApiSecuritySchemeType.Http,
                    BearerFormat = "JWT",
                });

                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
            });
 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // �ҥ�middlewares ���� Swagger �W��M SwaggerUI
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(config => // serve ReDoc UI
            {
                // �]�w ReDoc UI ������(�`�N�n�[ / �׽u�}�Y)
                config.Path = "/redoc";
            });

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
