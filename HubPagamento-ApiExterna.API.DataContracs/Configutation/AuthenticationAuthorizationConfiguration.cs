using HubPagamento.ApiExterna.IoC.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HubPagamento.ApiExterna.API.DataContracs.Configutation
{
    public static class AuthenticationAuthorizationConfiguration
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = true;
                 options.SaveToken = true;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidIssuer = appSettings.JwtSettings.Issuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSettings.Secret))
                 };

                 options.Events = new JwtBearerEvents()
                 {
                     OnAuthenticationFailed = async context =>
                     {
                         await Task.Run(() =>
                         {
                             if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                             {
                                 context.Response.Headers.Add("Token-Expired", "true");
                             }
                         });
                     }
                 };
             });

            return services;
        }
    }
}
