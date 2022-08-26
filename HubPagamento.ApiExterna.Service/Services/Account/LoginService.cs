using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Requests;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HubPagamento.ApiExterna.Service.Services.Account
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<ILoginService> _logger;
        private readonly ILoginApiResponseFactory _factoryResponse;
        private readonly HttpClient _httpClient;

        public LoginService(IOptions<AppSettings> settings, ILogger<ILoginService> logger, HttpClient httpClient, ILoginApiResponseFactory factoryResponse)
        {
            _settings = settings.Value;
            _logger = logger;
            _httpClient = httpClient;
            _factoryResponse = factoryResponse;
        }

        public async Task<BaseResponse> Login(string service, string password)
        {
            var jsonContent = JsonSerializer.Serialize(new LoginRequest(service, password), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Chamando Endpoint de login workflow");

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.WorkFlowApi.BaseURL + _settings.WorkFlowApi.Login, requestContent);

            var loginResponse = await this._factoryResponse.BuildResponse(response);

            return loginResponse;
        }
    }
}
