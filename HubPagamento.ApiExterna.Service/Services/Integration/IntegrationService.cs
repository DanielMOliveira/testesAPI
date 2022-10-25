using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
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
            
            var jsonContent = JsonSerializer.Serialize(new { cardM4U.Month, cardM4U.Pan, cardM4U.Partner, cardM4U.Year }, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Chamando Endpoint de Tokenização M4U");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cardM4U.Token);

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.IntegrationM4UApi.M4UCardsEndPoint, requestContent);

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Finalizando chamada do Endpoint de Tokenização M4U com status {response.StatusCode}");

            var tokenizedCard = await this._integrationFactory.BuildResponse(response);

            return tokenizedCard;
        }

        public async Task<BaseResponse> InvokeLoginM4UAsync(LoginM4UDTO login)
        {
            var jsonContent = JsonSerializer.Serialize(login, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Chamando Endpoint de Autenticação M4U");

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login.User}:{login.Password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.IntegrationM4UApi.M4UAuth, requestContent);

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Finalizando chamada do Endpoint de Autenticação M4U");

            var loginAuth = await this._integrationFactory.BuildResponse(response);

            return loginAuth;
        }
    }
}
