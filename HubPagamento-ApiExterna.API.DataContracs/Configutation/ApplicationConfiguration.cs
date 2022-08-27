using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Factories;
using HubPagamento.ApiExterna.Service.Services.Card;
using Microsoft.Extensions.DependencyInjection;
using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.API.DataContracs.Validators;
using HubPagamento.ApiExterna.API.Application;
using FluentValidation;
using HubPagamento.ApiExterna.API.DataContracs.Requests;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using HubPagamento.ApiExterna.Service.Services.Integration;
using HubPagamento.ApiExterna.Service.Services.Account;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Configutation
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            #region Factories
            services.AddScoped<IWorkFlowApiResponseFactory, WorkFlowApiResponseFactory>();
            services.AddScoped<IIntegrationApiResponseFactory, IntegrationApiResponseFactory>();
            services.AddScoped<ILoginApiResponseFactory, LoginApiResponseFactory>();
            #endregion

            #region Services
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IIntegrationService, IntegrationService>();
            services.AddScoped<ILoginService, LoginService>();
            #endregion

            #region Validators
            services.AddScoped<IValidator<AddCardCommand>, AddCardValidator>();
            services.AddScoped<IValidator<CardDTO>, CardDTOValidator>();
            services.AddScoped<IValidator<CustomerDTO>, CustomerDTOValidator>();
            services.AddScoped<IValidator<TokenizeCardCommand>, TokenizeCardValidator>();
            #endregion

            return services;
        }

        public static IServiceCollection AddHttpClientConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddHttpClient<ICardService, CardService>(client =>
            {
                client.BaseAddress = new Uri(appSettings.WorkFlowApi.BaseURL);
                client.Timeout = TimeSpan.FromSeconds(60);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            services.AddHttpClient<IIntegrationService, IntegrationService>(client =>
            {
                client.BaseAddress = new Uri(appSettings.IntegrationM4UApi.M4UBaseURL);
                client.Timeout = TimeSpan.FromSeconds(60);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            return services;
        }

        public static IServiceCollection ControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(option =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var response = new
                    {
                        action = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName,
                        errors = context.ModelState.Keys.Select(currentField =>
                        {
                            return new
                            {
                                field = currentField,
                                Messages = context.ModelState[currentField]?.Errors.Select(e => e.ErrorMessage)
                            };
                        })
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static AppSettings ConfigureAppSettings(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));

            ArgumentNullException.ThrowIfNull(appSettingsSection);

            builder.Services.Configure<AppSettings>(appSettingsSection);

            return appSettingsSection.Get<AppSettings>();
        }
    }
}
