using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.Service.Services.Integration
{
    public class IntegrationService : IIntegrationService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly ILogger<IIntegrationService> _logger;
        private readonly IIntegrationApiResponseFactory _integrationFactory;

        public IntegrationService(IOptions<AppSettings> settings, ILogger<IIntegrationService> logger, HttpClient httpClient, IIntegrationApiResponseFactory integrationFactory)
        {
            _settings = settings.Value;
            _httpClient = httpClient;
            _logger = logger;
            _integrationFactory = integrationFactory;
        }

        public async Task<BaseResponse> InvokeBemobiM4UAsync(CardM4UBemodiDTO cardM4U)
        {
            
            var jsonContent = JsonSerializer.Serialize(cardM4U, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Chamando Endpoint de Tokenização M4U");

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.IntegrationM4UApi.M4UBaseURL + _settings.IntegrationM4UApi.M4UCardsEndPoint, requestContent);

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Finalizando chamada do Endpoint de Tokenização M4U");

            var tokenizedCard = await this._integrationFactory.BuildResponse(response);

            return tokenizedCard;
        }
    }
}
