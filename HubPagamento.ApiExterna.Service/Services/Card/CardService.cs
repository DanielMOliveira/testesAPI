using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace HubPagamento.ApiExterna.Service.Services.Card
{
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly IWalletApiResponseFactory _walletApiResponseFactory;
        private readonly ILogger<ICardService> _logger;
        public CardService(IOptions<AppSettings> settings, ILogger<ICardService> logger, HttpClient httpClient, IWalletApiResponseFactory walletApiResponseFactory)
        {
            _walletApiResponseFactory = walletApiResponseFactory;
            _settings = settings?.Value;
            _httpClient = httpClient;   
            _logger = logger;
        }

        public async Task<AddCardResponse> InvokeAddCard(AddCardDTO infoCard)
        {
            var jsonContent = JsonSerializer.Serialize(infoCard);
            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation("Iniciando chamada para adicionar o cartão à carteira");

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.WalletApi.BaseURL, requestContent);

            var addCardresponse = await this._walletApiResponseFactory.BuildResponse(response);

            return addCardresponse;
        }
    }
}