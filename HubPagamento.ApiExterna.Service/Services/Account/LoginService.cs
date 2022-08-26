using HubPagamento.ApiExterna.IoC.Configuration;
using HubPagamento.ApiExterna.Service.Contracts;
using HubPagamento.ApiExterna.Service.Contracts.Factories;
using HubPagamento.ApiExterna.Service.Requests;
using HubPagamento.ApiExterna.Service.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HubPagamento.ApiExterna.Service.Services.Account
{
    public class LoginService : ILoginService
    {
        private readonly AppSettings _settings;
        private readonly ILogger<ILoginService> _logger;
        private readonly ILoginApiResponseFactory _factory;
        private readonly HttpClient _httpClient;
        public LoginService(IOptions<AppSettings> settings, ILogger<ILoginService> logger, HttpClient httpClient)
        {
            _settings = settings.Value;
            _logger = logger;
            _httpClient = httpClient;
        }
        public async Task<AuthorizeResponse> Login(string service, string password)
        {
            var jsonContent = JsonSerializer.Serialize(new LoginRequest(service, password));
            var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Chamando Endpoint de login workflow");

            HttpResponseMessage response = await _httpClient.PostAsync(_settings.WorkFlowApi.BaseURL + _settings.WorkFlowApi.Login, requestContent);

            _logger.LogInformation($"[{DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")}] Finalizando chamada Endpoint de login workflow");

            var loginResponse = await this._factory.BuildResponse(response);

            return loginResponse;

        }
    }
}
