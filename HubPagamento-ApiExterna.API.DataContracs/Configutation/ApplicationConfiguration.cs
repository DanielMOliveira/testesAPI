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

namespace HubPagamento.ApiExterna.API.Configutation
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Factories
            services.AddScoped<IWalletApiResponseFactory, WalletApiResponseFactory>();
            services.AddScoped<IIntegrationApiResponseFactory, IntegrationApiResponseFactory>();
            #endregion

            #region Services
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IIntegrationService, IntegrationService>();
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
                client.BaseAddress = new Uri(appSettings.WalletApi.BaseURL); ;
                client.Timeout = TimeSpan.FromSeconds(60);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            services.AddHttpClient<IIntegrationService, IntegrationService>(client =>
            {
                client.BaseAddress = new Uri(appSettings.IntegrationApi.M4UBaseURL); ;
                client.Timeout = TimeSpan.FromSeconds(60);
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));

            return services;
        }


    }
}
