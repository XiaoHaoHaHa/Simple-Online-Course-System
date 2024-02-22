using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace CourseApi.Middleware
{
    public static class JwtAuthExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT Token based Authentication         
            // Install Microsoft.AspNetCore.Authentication.JwtBearer package
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.Events = new JwtBearerEvents();

                //JWT檢驗失敗的回應處理
                options.Events.OnChallenge = context =>
                {
                    // 當驗證失敗時，回應標頭包含 WWW-Authenticate 標頭，顯⽰失敗的詳 細原因
                    options.IncludeErrorDetails = true;

                    context.HandleResponse(); context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 401;
                    var responseContent = JsonConvert.SerializeObject(new { errorMessage = "invalid token" });

                    return context.Response.WriteAsync(responseContent);
                };

                //設定要檢驗的JWT內容
                options.TokenValidationParameters = new TokenValidationParameters
                {                     
                    ValidateIssuer = true,                     
                    ValidateAudience = true,                     
                    ValidIssuer = configuration["TokenSettings:Issuer"],                     
                    ValidAudience = configuration["TokenSettings:Audience"],                     
                    ValidateLifetime = true,                     
                    ValidateIssuerSigningKey = true,                     
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSettings:IssuerSigningKey"])),                    
                    RequireExpirationTime = true                 
                }; 
            });
        }
    }
}
