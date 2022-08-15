using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Factories;
using HubPagamento.ApiExterna.Service.Services.Card;
using Microsoft.Extensions.DependencyInjection;
using HubPagamento.ApiExterna.IoC.Configuration;

namespace HubPagamento.ApiExterna.API.Configutation
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Factories
            services.AddScoped<IWalletApiResponseFactory, WalletApiResponseFactory>();
            #endregion

            #region Services
            services.AddScoped<ICardService, CardService>();
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

            return services;
        }
    }
}
