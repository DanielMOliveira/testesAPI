using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.DTO;
using HubPagamento.ApiExterna.Service.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.Service.Services.Card
{
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        private readonly IWorkFlowApiResponseFactory _workFlowApiResponseFactory;
        private readonly ILogger<ICardService> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;

        public CardService(IOptions<AppSettings> settings, ILogger<ICardService> logger, HttpClient httpClient, IWorkFlowApiResponseFactory walletApiResponseFactory, IHttpContextAccessor httpContextAccessor)
        {
            _workFlowApiResponseFactory = walletApiResponseFactory;
            _settings = settings.Value;
            _httpClient = httpClient;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddCardResponse> InvokeAddCard(AddCardDTO infoCard)
        {
            var jsonContent = JsonSerializer.Serialize(infoCard, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{ DateTime.Now.ToString("dd/MM/yyyy H:mm:ss") }] Iniciando chamada para adicionar o cartão à carteira");

            var accessToken = _httpContextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString();

            _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.WorkFlowApi.BaseURL + _settings.WorkFlowApi.CardEndpoint, requestContent);

            var addCardresponse = await this._workFlowApiResponseFactory.BuildResponse(response);

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Retorno da chamada da workflow");
          
            return addCardresponse;
        }
    }
}