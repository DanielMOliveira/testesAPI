﻿using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace HubPagamento.ApiExterna.Service.Services.Integration
{
    public class IntegrationService : IIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings? _settings;
        private readonly ILogger<IIntegrationService> _logger;
        private readonly IIntegrationApiResponseFactory _integrationFactory;

        public IntegrationService(IOptions<AppSettings> settings, ILogger<IIntegrationService> logger, HttpClient httpClient, IIntegrationApiResponseFactory integrationFactory)
        {
            _settings = settings.Value;
            _httpClient = httpClient;
            _logger = logger;
            _integrationFactory = integrationFactory;
        }

        public async Task<BemobiM4UIntegrationResponse> InvokeBemobiM4UAsync(CardM4UBemodiDTO cardM4U)
        {
            
            var jsonContent = JsonSerializer.Serialize(cardM4U);
            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation("Chamando Endpoint de Tokenização M4U");

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.IntegrationApi.M4UBaseURL, requestContent);

            var tokenizedCard = await this._integrationFactory.BuildResponse(response);

            return tokenizedCard;
        }
    }
}
